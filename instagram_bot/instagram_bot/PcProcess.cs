using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using instagram_bot.Properties;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Timers;
namespace instagram_bot
{
    class PcProcess
    {
        public static Bitmap eklevpn = Resources.vpntamam_1_;
        public static Bitmap vpntamam = Resources.vpntamam2_1_;
        public static Bitmap surfsharkac = Resources.surf_1_;
        public static Bitmap suremail = Resources.surfeposta_1_;
        public static Bitmap surpass = Resources.surfparola_1_;
        public static Bitmap surlogin = Resources.surfgirisyap_1_;
        public static Bitmap surlocations = Resources.surlocation2;
        public static Bitmap surargentina = Resources.surargentina2;
        public static Bitmap survpnbaglandi = Resources.vpnbaglandi;
        public static Bitmap survpnpencerekapat = Resources.surfvpnpencerekapat_2_;
        public static Bitmap instakaydol = Resources.instakaydol_1_;
        public static Bitmap hesapvar = Resources.hesapvar;
        public static Bitmap acilishata = Resources.hatapng;
        public static Bitmap dogumgunu = Resources.dogumgunu;
        public static Bitmap bildirim = Resources.bildirimleriac;
        public static Bitmap _3cizgi = Resources._3cizgi_1_;
        public static Bitmap gecmis2 = Resources.gecmis_1_;
        public static Bitmap yakingecmistemizle2 = Resources.yakingecmis_1_;
        public static Bitmap gecmiszaman = Resources.gecmiszaman_1_;
        public static Bitmap gecmishersey = Resources.gecmiszamanhersey_1_;
        public static Bitmap gecmissitetercih = Resources.siteayarları_1_;
        public static Bitmap gecmiscevrimdisi = Resources.siteayarları2_1_;
        public static Bitmap tamam2 = Resources.tamam_1_;
        public static Bitmap cerezizin = Resources.cerezizin_1_;




        public static PcProcess pcProcess;

        public PcProcess()
        {
            pcProcess = this;
        }

        public Rectangle FindImageOnScreen(Bitmap bmpMatch, Bitmap screenCapture, bool ExactMatch)
        {
            Rectangle rct = Rectangle.Empty;
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Bitmap ScreenBmp = screenCapture;



                BitmapData ImgBmd = bmpMatch.LockBits(new Rectangle(0, 0, bmpMatch.Width, bmpMatch.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData ScreenBmd = ScreenBmp.LockBits(new Rectangle(0, 0, ScreenBmp.Width, ScreenBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                byte[] ImgByts = new byte[(Math.Abs(ImgBmd.Stride) * bmpMatch.Height) - 1 + 1];
                byte[] ScreenByts = new byte[(Math.Abs(ScreenBmd.Stride) * ScreenBmp.Height) - 1 + 1];

                Marshal.Copy(ImgBmd.Scan0, ImgByts, 0, ImgByts.Length);
                Marshal.Copy(ScreenBmd.Scan0, ScreenByts, 0, ScreenByts.Length);

                bool FoundMatch = false;

                int sindx, iindx;
                int spc, ipc;

                int skpx = System.Convert.ToInt32((bmpMatch.Width - 1) / (double)10);
                if (skpx < 1 | ExactMatch)
                    skpx = 1;
                int skpy = System.Convert.ToInt32((bmpMatch.Height - 1) / (double)10);
                if (skpy < 1 | ExactMatch)
                    skpy = 1;

                for (int si = 0; si <= ScreenByts.Length - 1; si += 3)
                {
                    FoundMatch = true;
                    for (int iy = 0; iy <= ImgBmd.Height - 1; iy += skpy)
                    {
                        for (int ix = 0; ix <= ImgBmd.Width - 1; ix += skpx)
                        {
                            sindx = (iy * ScreenBmd.Stride) + (ix * 3) + si;
                            iindx = (iy * ImgBmd.Stride) + (ix * 3);
                            spc = Color.FromArgb(ScreenByts[sindx + 2], ScreenByts[sindx + 1], ScreenByts[sindx]).ToArgb();
                            ipc = Color.FromArgb(ImgByts[iindx + 2], ImgByts[iindx + 1], ImgByts[iindx]).ToArgb();
                            if (spc != ipc)
                            {
                                FoundMatch = false;
                                iy = ImgBmd.Height - 1;
                                ix = ImgBmd.Width - 1;
                            }
                        }
                    }
                    if (FoundMatch)
                    {
                        double r = si / (double)(ScreenBmp.Width * 3);
                        double c = ScreenBmp.Width * (r % 1);
                        if (r % 1 >= 0.5)
                            r -= 1;
                        rct.X = System.Convert.ToInt32(c);
                        rct.Y = System.Convert.ToInt32(r);
                        rct.Width = bmpMatch.Width;
                        rct.Height = bmpMatch.Height;
                        break;
                    }
                }

                bmpMatch.UnlockBits(ImgBmd);
                ScreenBmp.UnlockBits(ScreenBmd);
                //ScreenBmp.Dispose();


                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                Debug(" Süre:" + ts);
                return rct;
            }
            catch (Exception e)
            {
                return rct;
            }
            return rct;

        }

        public Bitmap GetScreenCapture()
        {
            Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics g = Graphics.FromImage(screenCapture);

            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                             Screen.PrimaryScreen.Bounds.Y,
                             0, 0,
                             screenCapture.Size,
                             CopyPixelOperation.SourceCopy);


            //screenCapture.Save("c:\\button1.png", System.Drawing.Imaging.ImageFormat.Png);




            return screenCapture;
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void DoMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }


        private void Debug(string text)
        {
            Console.WriteLine(text);

        }

        public static Bitmap ulkeresmigetir(string ulkeismi)
        {
            Bitmap donenulkeresmi = Resources.surargentina;
            string donenulke = ""; //işe yaramıyor

            if (ulkeismi == "Arjantin") { donenulke = "Arjantin"; donenulkeresmi = Resources.surargentina2; }
            if (ulkeismi == "Arnavutluk") { donenulke = "Arnavutluk"; donenulkeresmi = Resources.arnavutluk; }
            if (ulkeismi == "Avustralya - Adelaide") { donenulke = "Adelaide"; donenulkeresmi = Resources.avustralya; }
            if (ulkeismi == "Avustralya - Melbourne") { donenulke = "Melbourne"; donenulkeresmi = Resources.avustralya; }
            if (ulkeismi == "Avustralya - Perth") { donenulke = "Perth"; donenulkeresmi = Resources.avustralya; }
            if (ulkeismi == "Avustralya - Brisbane") { donenulke = "Brisbane"; donenulkeresmi = Resources.avustralya; }
            if (ulkeismi == "Avustralya - Sidney") { donenulke = "Sidney"; donenulkeresmi = Resources.avustralya; }
            if (ulkeismi == "Avusturya") { donenulke = "Avusturya"; donenulkeresmi = Resources.Avusturya; }
            if (ulkeismi == "Azerbaycan") { donenulke = "Azerbaycan"; donenulkeresmi = Resources.Azerbaycan; }
            if (ulkeismi == "Belçika - Antwerp") { donenulke = "Antwerp"; donenulkeresmi = Resources.belcika; }
            if (ulkeismi == "Belçika - Brüksel") { donenulke = "Brüksel"; donenulkeresmi = Resources.belcika; }
            if (ulkeismi == "Bosna ve Hersek") { donenulke = "Bosna ve Hersek"; donenulkeresmi = Resources.bosna; }
            if (ulkeismi == "Brezilya") { donenulke = "Brezilya"; donenulkeresmi = Resources.brezilya; }
            if (ulkeismi == "Bulgaristan") { donenulke = "Bulgaristan"; donenulkeresmi = Resources.bulgaristan; }
            if (ulkeismi == "Kanada - Toronto") { donenulke = "Toronto"; donenulkeresmi = Resources.kanada; }
            if (ulkeismi == "Kanada - Montreal") { donenulke = "Montreal"; donenulkeresmi = Resources.kanada; }
            if (ulkeismi == "Kanada - Vancouver") { donenulke = "Vancouver"; donenulkeresmi = Resources.kanada; }
            if (ulkeismi == "Şili") { donenulke = "Şili"; donenulkeresmi = Resources.sili; }
            if (ulkeismi == "Kolombiya") { donenulke = "Kolombiya"; donenulkeresmi = Resources.kolombiya; }
            if (ulkeismi == "Kosta Rika") { donenulke = "Kosta Rika"; donenulkeresmi = Resources.kostarika; }
            if (ulkeismi == "Hırvatistan") { donenulke = "Hırvatistan"; donenulkeresmi = Resources.hırvatistan; }
            if (ulkeismi == "Kıbrıs Rum Kesimi") { donenulke = "Kıbrıs Rum Kesimi"; donenulkeresmi = Resources.kibris; }
            if (ulkeismi == "Çek Cumhuriyeti") { donenulke = "Çek Cumhuriyeti"; donenulkeresmi = Resources.cek; }
            if (ulkeismi == "Danimarka") { donenulke = "Danimarka"; donenulkeresmi = Resources.danimarka; }
            if (ulkeismi == "Estonya") { donenulke = "Estonya"; donenulkeresmi = Resources.estonya; }
            if (ulkeismi == "Finlandiya") { donenulke = "Finlandiya"; donenulkeresmi = Resources.finlandiya; }
            if (ulkeismi == "Fransa - Marsilya") { donenulke = "Marsilya"; donenulkeresmi = Resources.fransa; }
            if (ulkeismi == "Fransa - Bordeaux") { donenulke = "Bordeaux"; donenulkeresmi = Resources.fransa; }
            if (ulkeismi == "Fransa - Paris") { donenulke = "Paris"; donenulkeresmi = Resources.fransa; }
            if (ulkeismi == "Gürcistan") { donenulke = "Gürcistan"; donenulkeresmi = Resources.gurcistan; }
            if (ulkeismi == "Yunanistan") { donenulke = "Yunanistan"; donenulkeresmi = Resources.yunanistan; }
            if (ulkeismi == "Hong Kong") { donenulke = "Hong Kong"; donenulkeresmi = Resources.hongkong; }
            if (ulkeismi == "Macaristan") { donenulke = "Macaristan"; donenulkeresmi = Resources.macaristan; }
            if (ulkeismi == "İzlanda") { donenulke = "İzlanda"; donenulkeresmi = Resources.izlanda; }
            if (ulkeismi == "Hindistan - Mumbai") { donenulke = "Mumbai"; donenulkeresmi = Resources.hindistan; }
            if (ulkeismi == "Hindistan - Chennai") { donenulke = "Chennai"; donenulkeresmi = Resources.hindistan; }
            if (ulkeismi == "Hindistan - Indore") { donenulke = "Indore"; donenulkeresmi = Resources.hindistan; }
            if (ulkeismi == "Endonezya") { donenulke = "Endonezya"; donenulkeresmi = Resources.endonezya; }
            if (ulkeismi == "İrlanda") { donenulke = "İrlanda"; donenulkeresmi = Resources.irlanda; }
            if (ulkeismi == "İsrail") { donenulke = "İsrail"; donenulkeresmi = Resources.israil; }
            if (ulkeismi == "İtalya - Milano") { donenulke = "Milano"; donenulkeresmi = Resources.italya; }
            if (ulkeismi == "İtalya - Roma") { donenulke = "Roma"; donenulkeresmi = Resources.italya; }
            if (ulkeismi == "Japonya") { donenulke = "Japonya"; donenulkeresmi = Resources.japonya; }
            if (ulkeismi == "Kazakistan") { donenulke = "Kazakistan"; donenulkeresmi = Resources.kazakistan; }
            if (ulkeismi == "Letonya") { donenulke = "Letonya"; donenulkeresmi = Resources.letonya; }
            if (ulkeismi == "Lüksemburg") { donenulke = "Lüksemburg"; donenulkeresmi = Resources.luksemburg_1_; }
            if (ulkeismi == "Malezya") { donenulke = "Malezya"; donenulkeresmi = Resources.malezya; }
            if (ulkeismi == "Meksika") { donenulke = "Meksika"; donenulkeresmi = Resources.meksika; }
            if (ulkeismi == "Moldova") { donenulke = "Moldova"; donenulkeresmi = Resources.moldova; }
            if (ulkeismi == "Hollanda") { donenulke = "Hollanda"; donenulkeresmi = Resources.hollanda; }
            if (ulkeismi == "Yeni Zelanda") { donenulke = "Yeni Zelanda"; donenulkeresmi = Resources.yenizelanda; }
            if (ulkeismi == "Nijerya") { donenulke = "Nijerya"; donenulkeresmi = Resources.nijerya; }
            if (ulkeismi == "Kuzey Makedonya") { donenulke = "Kuzey Makedonya"; donenulkeresmi = Resources.kuzeymakedonya; }
            if (ulkeismi == "Norveç") { donenulke = "Norveç"; donenulkeresmi = Resources.norveç; }
            if (ulkeismi == "Panama") { donenulke = "Panama"; donenulkeresmi = Resources.panama; }
            if (ulkeismi == "Peru") { donenulke = "Peru"; donenulkeresmi = Resources.peru; }
            if (ulkeismi == "Filipinler") { donenulke = "Filipinler"; donenulkeresmi = Resources.filipinler; }
            if (ulkeismi == "Polonya - Gdansk") { donenulke = "Gdansk"; donenulkeresmi = Resources.polonya; }
            if (ulkeismi == "Polonya - Varşova") { donenulke = "Varşova"; donenulkeresmi = Resources.polonya; }
            if (ulkeismi == "Portekiz - Porto") { donenulke = "Porto"; donenulkeresmi = Resources.portekiz; }
            if (ulkeismi == "Portekiz - Lizbon") { donenulke = "Lizbon"; donenulkeresmi = Resources.portekiz; }
            if (ulkeismi == "Romanya") { donenulke = "Romanya"; donenulkeresmi = Resources.romanya; }
            if (ulkeismi == "Rusya") { donenulke = "Rusya"; donenulkeresmi = Resources.rusya; }
            if (ulkeismi == "Sırbistan") { donenulke = "Sırbistan"; donenulkeresmi = Resources.sırbistan; }
            if (ulkeismi == "Singapur") { donenulke = "Singapur"; donenulkeresmi = Resources.singapur; }
            if (ulkeismi == "Slovakya") { donenulke = "Slovakya"; donenulkeresmi = Resources.slovakya; }
            if (ulkeismi == "Slovenya") { donenulke = "Slovenya"; donenulkeresmi = Resources.slovenya; }
            if (ulkeismi == "Güney Afrika") { donenulke = "Güney Afrika"; donenulkeresmi = Resources.güneyafrika; }
            if (ulkeismi == "Güney Kore") { donenulke = "Güney Kore"; donenulkeresmi = Resources.güneykore; }
            if (ulkeismi == "İspanya - Barselona") { donenulke = "Barselona"; donenulkeresmi = Resources.ispanya; }
            if (ulkeismi == "İspanya - Madrid") { donenulke = "Madrid"; donenulkeresmi = Resources.ispanya; }
            if (ulkeismi == "İspanya - Valencia") { donenulke = "Valencia"; donenulkeresmi = Resources.ispanya; }
            if (ulkeismi == "İsveç") { donenulke = "İsveç"; donenulkeresmi = Resources.isveç; }
            if (ulkeismi == "İsviçre") { donenulke = "İsviçre"; donenulkeresmi = Resources.isviçre; }
            if (ulkeismi == "Tayvan") { donenulke = "Tayvan"; donenulkeresmi = Resources.tayvan; }
            if (ulkeismi == "Tayland") { donenulke = "Tayland"; donenulkeresmi = Resources.tayland; }
            if (ulkeismi == "Türkiye") { donenulke = "Türkiye"; donenulkeresmi = Resources.turkiye; }
            if (ulkeismi == "Ukrayna") { donenulke = "Ukrayna"; donenulkeresmi = Resources.ukrayna; }
            if (ulkeismi == "Birleşik Arap Emirlikleri") { donenulke = "Birleşik Arap Emirlikleri"; donenulkeresmi = Resources.birlesikarapemirlikleri; }
            if (ulkeismi == "İngiltere - Glasgow") { donenulke = "Glasgow"; donenulkeresmi = Resources.ingiltere; }
            if (ulkeismi == "İngiltere - Londra") { donenulke = "Londra"; donenulkeresmi = Resources.ingiltere; }
            if (ulkeismi == "İngiltere - Manchester") { donenulke = "Manchester"; donenulkeresmi = Resources.ingiltere; }
            if (ulkeismi == "Birleşik Devletler - Charlotte") { donenulke = "Charlotte"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Miami") { donenulke = "Miami"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Seattle") { donenulke = "Seattle"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Buffalo") { donenulke = "Buffalo"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Orlando") { donenulke = "Orlando"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Detroit") { donenulke = "Detroit"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Los Angeles") { donenulke = "Los Angeles"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Latham") { donenulke = "Latham"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Ashburn") { donenulke = "Ashburn"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Denver") { donenulke = "Denver"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Atlanta") { donenulke = "Atlanta"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Tampa") { donenulke = "Tampa"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Bend") { donenulke = "Bend"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - San Francisco") { donenulke = "San Francisco"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Salt Lake City") { donenulke = "Salt Lake City"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Las Vegas") { donenulke = "Las Vegas"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Phoenix") { donenulke = "Phoenix"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - St. Louis") { donenulke = "St. Louis"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Dallas") { donenulke = "Dallas"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Houston") { donenulke = "Houston"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Şikago") { donenulke = "Şikago"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - New York") { donenulke = "New York"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Boston") { donenulke = "Boston"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Birleşik Devletler - Kansas City") { donenulke = "Kansas City"; donenulkeresmi = Resources.birleşikdevletler; }
            if (ulkeismi == "Venezuela") { donenulke = "Venezuela"; donenulkeresmi = Resources.venezuela; }



            return donenulkeresmi;
        }



    }
}
