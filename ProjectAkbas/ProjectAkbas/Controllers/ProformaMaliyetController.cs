using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAkbas.Data;
using ProjectAkbas.Models;
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
        // Proforma Maliyetler verisini çek
        var proformaMaliyetler = await _context2.ProformaMaliyetler
              .OrderByDescending(p => p.MalGrubu)
    .ThenBy(p => p.SiraBenzerKumas)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        // Kumas Kodlarını listele ve temizle
        var kumasKodlari = proformaMaliyetler
            .Select(p => p.KumasKodu.Trim()) // Trim boşlukları temizler
            .ToList();

        // Verileri sıralı bir şekilde çek
        var maraData = await _context3.Maras
            .Where(m => kumasKodlari.Contains(m.MATNR.Trim()))
            .ToListAsync();

        var sapIthalatVerileri = await _context1.SapIthalatVerileri
            .Where(si => kumasKodlari.Contains(si.Kumas.Trim()))
            .ToListAsync();

        var sapGuncelStokVerileri = await _context1.SapGuncelStokVerileri
            .Where(sg => kumasKodlari.Contains(sg.Kumas.Trim()))
            .ToListAsync();

        // Tekrar eden anahtarları kontrol et
        var sapGuncelStokVerileriDict = sapGuncelStokVerileri
            .GroupBy(sg => sg.Kumas.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var maraDataDict = maraData
            .GroupBy(m => m.MATNR.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var sapIthalatVerileriDict = sapIthalatVerileri
            .GroupBy(si => si.Kumas.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        // ProformaMaliyetDto oluştur
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

            ZGRUP = maraDataDict.TryGetValue(p.KumasKodu.Trim(), out m) ? m.ZGRUP : null
        }).ToList();

        // Toplam öğe sayısını hesapla
        var totalItems = await _context2.ProformaMaliyetler.CountAsync();

        // ViewModel oluştur ve gönder
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
