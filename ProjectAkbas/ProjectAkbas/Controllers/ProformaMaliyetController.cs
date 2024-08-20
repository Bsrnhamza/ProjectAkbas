using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAkbas.Data;
using ProjectAkbas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProformaMaliyetController : Controller
{
    private readonly ApplicationDbContext1 _context1;
    private readonly ApplicationDbContext2 _context2;
    private readonly ApplicationDbContext3 _context3;

    public ProformaMaliyetController(ApplicationDbContext1 context1, ApplicationDbContext2 context2, ApplicationDbContext3 context3)
    {
        _context1 = context1;
        _context2 = context2;
        _context3 = context3;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 150)
    {
        // PM SQL KODUNU OLUŞTUR
        var sqlQuery = $@"
        WITH ProformaMaliyetData AS (
            SELECT 
                KumasKodu, Qualities, IDD, MalGrubu, SiraBenzerKumas, KumasKalitesi, 
                HamKumasFiyatiMt, DokumaOrme, YurtDisiDolarMetre, YurticiDolarMetre, 
                ProfSFYurtici, ProfSFYurtdisiIng, Islem,
                ROW_NUMBER() OVER (ORDER BY MalGrubu DESC, SiraBenzerKumas ASC) AS RowNum
            FROM [Akb_Proforma_Test].[dbo].[ProformaMaliyet]
        )
        SELECT 
            KumasKodu, Qualities, IDD, MalGrubu, SiraBenzerKumas, KumasKalitesi, 
            HamKumasFiyatiMt, DokumaOrme, YurtDisiDolarMetre, YurticiDolarMetre, 
            ProfSFYurtici, ProfSFYurtdisiIng, Islem
        FROM ProformaMaliyetData
        WHERE RowNum BETWEEN {(pageNumber - 1) * pageSize + 1} AND {pageNumber * pageSize}";

        // PM SORGUSUNU SQL ÇEK 
        var proformaMaliyetler = await _context2.ProformaMaliyetler
            .FromSqlRaw(sqlQuery)
            .AsNoTracking()
            .ToListAsync();

        // KumasKod Listele
        var kumasKodlari = proformaMaliyetler
            .Select(p => p.KumasKodu.Trim())
            .Distinct()
            .ToList();

        // KK sorgusunu çevir
        var kodlarInClause = string.Join(",", kumasKodlari.Select(k => $"'{k}'"));

        // Mara Data sorgusu
        var maraData = await _context3.Maras
            .FromSqlRaw($@"
            SELECT 
                MATNR, ZKOMP_ORM1, ZKOMP_ORM_ORAN1, ZKOMP_ORM2, ZKOMP_ORM_ORAN2, 
                ZKOMP_ORM3, ZKOMP_ORM_ORAN3, ZKOMP_ORM4, ZKOMP_ORM_ORAN4, ZKOMP_ORM5,ZKOMP_ORM_ORAN5,
                ZMML_EN, ZMML_GRM, ZGRUP
            FROM aep.MARA
            WHERE MATNR IN ({kodlarInClause})")
            .ToListAsync();

        // SIV sorgusu
        var sapIthalatVerileri = await _context1.SapIthalatVerileri
            .FromSqlRaw($@"
            SELECT 
                KUMAS, Sipariste, Antrepoda
            FROM SapIthalatVerileri
            WHERE KUMAS IN ({kodlarInClause})")
            .ToListAsync();

        // SGSV sorgusu
        var sapGuncelStokVerileri = await _context1.SapGuncelStokVerileri
            .FromSqlRaw($@"
            SELECT 
                KUMAS, StokMiktarı
            FROM SapGuncelStokVerileri
            WHERE KUMAS IN ({kodlarInClause})")
            .ToListAsync();

        // ZGTS sorgusu
        var zmmGrupTs = await _context3.ZmmGrupTs
            .ToListAsync();

        // Anahtar Kontrol
        var sapGuncelStokVerileriDict = sapGuncelStokVerileri
            .GroupBy(sg => sg.Kumas.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var maraDataDict = maraData
            .GroupBy(m => m.MATNR.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var sapIthalatVerileriDict = sapIthalatVerileri
            .GroupBy(si => si.Kumas.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var zmmGrupDict = zmmGrupTs
            .GroupBy(zg => zg.zmGrup.Trim())
            .ToDictionary(g => g.Key, g => g.First().zmGrupAdiEn);

        // Dto oluştur
        var proformaMaliyetDtos = proformaMaliyetler.Select(p => new ProformaMaliyetDto
        {
            Qualities = p.Qualities,
            IDD = p.IDD,
            MalGrubu = p.MalGrubu,
            SiraBenzerKumas = p.SiraBenzerKumas,
            KumasKodu = p.KumasKodu,
            KumasKalitesi = p.KumasKalitesi,
            HamKumasFiyatiMt = p.HamKumasFiyatiMt,
            DokumaOrme = p.DokumaOrme,
            YurtDisiDolarMetre = p.YurtDisiDolarMetre,
            YurticiDolarMetre = p.YurticiDolarMetre,
            ProfSFYurtici = p.ProfSFYurtici,
            ProfSFYurtdisiIng = p.ProfSFYurtdisiIng,
            Islem = p.Islem,

            Sipariste = sapIthalatVerileriDict.TryGetValue(p.KumasKodu.Trim(), out var si) ? si.Sipariste : null,
            Antrepoda = sapIthalatVerileriDict.TryGetValue(p.KumasKodu.Trim(), out si) ? si.Antrepoda : null,
            StokMiktarı = sapGuncelStokVerileriDict.TryGetValue(p.KumasKodu.Trim(), out var sg) ? sg.StokMiktarı : null,

            ZKOMP_ORM1 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out var m) ? m.ZKOMP_ORM1 : null,
            ZKOMP_ORM_ORAN1 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM_ORAN1 : null,
            ZKOMP_ORM2 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM2 : null,
            ZKOMP_ORM_ORAN2 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM_ORAN2 : null,
            ZKOMP_ORM3 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM3 : null,
            ZKOMP_ORM_ORAN3 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM_ORAN3 : null,
            ZKOMP_ORM4 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM4 : null,
            ZKOMP_ORM_ORAN4 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM_ORAN4 : null,
            ZKOMP_ORM5 = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZKOMP_ORM5 : null,
            ZMML_EN = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZMML_EN : null,
            ZMML_GRM = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZMML_GRM : null,

            ZGRUP = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ?
            (zmmGrupDict.TryGetValue(m.ZGRUP.Trim(), out var zmGrupAdiEn) ? zmGrupAdiEn : null)
            : null
        }).ToList();

        // öğre sayısıın hesapla
        var totalItems = await _context2.ProformaMaliyetler.CountAsync();
        var viewModelForRendering = new ProformaMaliyetViewModel
        {
            ProformaMaliyetler = proformaMaliyetDtos,
            PagingInfo = new PagingInfo
            {
                CurrentPage = pageNumber,
                TotalItems = totalItems,
                ItemsPerPage = pageSize
            }
        };

        return View(viewModelForRendering);
    }
}
