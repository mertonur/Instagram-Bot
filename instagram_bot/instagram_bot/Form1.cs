using instagram_bot.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace instagram_bot
{
    public partial class Form1 : Form
    {
        static IWebDriver driver = new FirefoxDriver();
        PcProcess pcProcess = new PcProcess();
        public static RandomHesap hesap = new RandomHesap();

        public Form1()
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            mysqlconn.Sunucu_MySql_Baglanti = mysqlconn.MySqlBaslat();

            
            instagramkayit();


        }

        public void ilkAcilis()
        {

        }

        public async Task<bool> instagramkayit()
        {
            driver.Quit();
            await Task.Delay(1000);
            driver = new FirefoxDriver();
            
            try
            {
                

                driver.Navigate().GoToUrl("https://www.google.com.tr/");

                await Task.Delay(5000);

                
                if (cerezlerisil())
                {
                    if (vpnkur()) {  }
                    else
                    {
                        await Task.Delay(105000);
                        throw new Exception("***Vpn Kurulamadı***");
                    }
                }
                else
                {
                    throw new Exception("***Çerezler Silinemedi***");
                }


                string mail = MailBaslangic("MailBaslangic", 1000, 3);
                if (mail == "") { throw new Exception("***Email Bulunamadı***"); }


                ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");

                driver.SwitchTo().Window(driver.WindowHandles.Last());






                driver.Navigate().GoToUrl("https://www.instagram.com/accounts/emailsignup/");




                await Task.Delay(1000);
                Bitmap tikla = PcProcess.cerezizin;

                if (ResimBul(tikla, "cerezizin", 1000, 5))
                {
                   
                }
                await Task.Delay(3000);

                hesap.randomisim();

                string isim = hesap.sonisim;
                string soyisim = hesap.sonsoyisim;
                string sifre = "şifre";//şifre
                // string mail = txtMail.Text;
                string kullaniciAdi = hesap.sonhesapismi;
                IWebElement name = driver.FindElement(By.Name("fullName"));
                name.SendKeys(isim + " " + soyisim);
                Debug(":"+isim + " " + soyisim, ConsoleColor.Green);
                await Task.Delay(1000);
                IWebElement username = driver.FindElement(By.Name("username"));
                username.SendKeys(kullaniciAdi);
                Debug(":" + kullaniciAdi, ConsoleColor.Green);
                await Task.Delay(1000);
                IWebElement email = driver.FindElement(By.Name("emailOrPhone"));
                email.SendKeys("asd");
                Debug(":" + "asd", ConsoleColor.Green);
                await Task.Delay(1000);
                
                IWebElement password = driver.FindElement(By.Name("password"));
                password.SendKeys(sifre);
                await Task.Delay(1000);

                IWebElement btnrefreshname = driver.FindElement(By.CssSelector(".coreSpriteInputRefresh"));
                btnrefreshname.Click();
                await Task.Delay(1000);

                kullaniciAdi = driver.FindElement(By.Name("username")).GetAttribute("value");
                Debug(kullaniciAdi + " Yeni Kullanici Adi", ConsoleColor.Green);
                email.SendKeys(OpenQA.Selenium.Keys.Backspace);
                email.SendKeys(OpenQA.Selenium.Keys.Backspace);
                email.SendKeys(OpenQA.Selenium.Keys.Backspace);
                // email.SendKeys(System.Windows.Forms.Keys.Control + "a");
                email.SendKeys(mail);


              
                tikla = PcProcess.instakaydol;

                if (ResimBul(tikla, "instakaydol", 1000, 5))
                {
                    //Debug("Tikladim instakaydol"); 
                }
                else { throw new Exception("***Kaydol Tuşu Bulunamadı***"); }

             


                await Task.Delay(4000);

                Random random = new Random();
                int ayrandom = random.Next(1, 12);
                int gunrandom = random.Next(1, 28);
                int yilrandom = random.Next(1980, 2000);


                                                                   
                IWebElement elementay = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/div/div[1]/div/div[4]/div/div/span/span[1]/select/option[" + ayrandom + "]"));


                elementay.Click();









                IWebElement elementgun = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/div/div[1]/div/div[4]/div/div/span/span[2]/select/option[" + gunrandom + "]"));
                elementgun.Click();

               
                IWebElement elementyil = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/div/div[1]/div/div[4]/div/div/span/span[3]/select/option[" + (2022 - yilrandom) + "]"));
                elementyil.Click();


               

                IWebElement btnileri = driver.FindElement(By.CssSelector(".L3NKy"));
                btnileri.Click();

                driver.SwitchTo().Window(driver.WindowHandles.First());



                await Task.Delay(25000);
                IWebElement btnyenilemail = driver.FindElement(By.CssSelector(".yenile-link"));
                btnyenilemail.Click();
                await Task.Delay(5000);

                string gelensifre = MailOnayKoduCek("MailOnayKoduCek", 5000, 10);
                if (gelensifre == "") { throw new Exception("***Email Çekilemedi***"); }

                gelensifre = gelensifre.Substring(0, 6);
                //Debug(gelensifre);
                Debug("Mail doğrulandı (" + gelensifre + ")", ConsoleColor.Green);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                IWebElement onayla = driver.FindElement(By.Name("email_confirmation_code"));
                onayla.SendKeys(gelensifre);

                IWebElement btnonayla = driver.FindElement(By.CssSelector(".L3NKy"));
                btnonayla.Click();

                //
                tikla = PcProcess.bildirim;
                if (ResimBul(tikla, "bildirim", 1000, 60)) { Debug("Hesap Başarıyla Açıldı", ConsoleColor.DarkGreen); }
                else { throw new Exception("***Hesap Açılamadı***"); }

                string makineid = "0";
                if (textBox1.Text == "")
                {

                }
                else
                {
                    makineid = textBox1.Text;
                }

                mysqlconn.kullaniciekle(isim, soyisim, kullaniciAdi, ayrandom.ToString(), gunrandom.ToString(), yilrandom.ToString(), makineid, sonulke, sonipadresi);
                await Task.Delay(1000);


                await instagramkayit();
                return true;
            }
            catch (Exception ex)
            {
                //Debug(ex);

                //enbaştan başlat
                Debug("Bidaha Başlatıldı", ConsoleColor.DarkRed);
                await instagramkayit();
                // lstLog.Items.Add("(YazbeeKayit) Hata: " + ex.Message);
                return false;
            }
        }

        string lastmail = "";
        string sonulke = "";

        public string MailOnayKoduCek(string funname, int beklemesuresi, int maxtekrarsayisi)
        {
            int ResimBulint = 1;
            string maildonen = "";
            while (ResimBulint <= maxtekrarsayisi && maildonen == "")
            {
                maildonen = MailOnayKoduCekFun();
                if (maildonen == "") { ResimBulint += 1; }
                else
                {
                    return maildonen;

                    //Debug("Mail Onay Kodu Buldum " + funname);
                }
                if (ResimBulint > maxtekrarsayisi) { throw new Exception("Mail Bulunamadı " + funname); }
                if (ResimBulint % 3 == 0)
                {
                    try
                    {

                        IWebElement btnyenilemail = driver.FindElement(By.CssSelector(".yenile-link"));
                        btnyenilemail.Click();
                    }
                    catch (Exception ex)
                    {
                        Debug("Mail Onay Kodu Çekilemedi (1)", ConsoleColor.Red);


                        return "";
                    }
                    System.Threading.Thread.Sleep(5000);
                }
                System.Threading.Thread.Sleep(beklemesuresi);
            }
            return maildonen;



        }

        public string MailOnayKoduCekFun()
        {
            try
            {

                return MailDogrula();

            }
            catch (Exception ex)
            {
                Debug("Mail Onay Kodu Çekilemedi (2)", ConsoleColor.Red);


                return "";
            }
        }

        public string MailCek()
        {
            try
            {
                driver.Navigate().GoToUrl("https://tempail.com/tr/gecici-mail/");

                IWebElement mail = driver.FindElement(By.XPath("//*[@id=\"eposta_adres\"]"));
                string mailAdresi = mail.GetAttribute("value");
                lastmail = mailAdresi;
                Debug("Alınan Mail Adresi:" + lastmail, ConsoleColor.Green);
                return lastmail;
            }
            catch (Exception ex)
            {
                Debug("(MailCek) Hata: " + ex.Message, ConsoleColor.Red);
                return "";
            }
        }

        public string MailDogrula()
        {

            try
            {

                IWebElement instaMail = driver.FindElement(By.CssSelector("div.baslik:nth-child(3)"));
                string ybt = instaMail.Text;
                //Debug(ybt);
                return ybt;


            }
            catch (Exception ex)
            {
                Debug("(MailDogrula) Hata: " + ex.Message, ConsoleColor.Red);
                return "";
            }
            return "";
        }


        public bool GorselBulTikla(Bitmap ekrangoruntusu, Bitmap aranicakresim)
        {

            Rectangle rect = PcProcess.pcProcess.FindImageOnScreen(aranicakresim, ekrangoruntusu, false);
            //Debug(rect.X.ToString());

            if (rect != Rectangle.Empty)//Image Foud
            {
                //Debug("ad finded");
                Point cntr = new Point(rect.X + System.Convert.ToInt32(rect.Width / (double)2), rect.Y + System.Convert.ToInt32(rect.Height / (double)2));
                Cursor.Position = cntr;
                //Debug(rect.ToString());

                PcProcess.DoMouseClick();

                return true;

            }
            else
            {
                Debug("Resim Bulunmadı", ConsoleColor.Red);
                return false;

            }

        }
        private void Debug(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public bool cerezlerisil()
        {
            try
            {
                Bitmap tikla = PcProcess._3cizgi;
                Bitmap ekrangoruntusu = PcProcess.pcProcess.GetScreenCapture();

                int cerezsilasama = 1;
                if (cerezsilasama == 1)
                {
                    if (ResimBul(tikla, "_3cizgi", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //Debug("Tikladim _3cizgi"); 
                    }

                }
                if (cerezsilasama == 2)
                {
                    System.Threading.Thread.Sleep(100);

                    tikla = PcProcess.gecmis2;

                    if (ResimBul(tikla, "gecmis2", 1000, 5))
                    {
                        cerezsilasama += 1;
                        // Debug("Tikladim gecmis2"); 
                    }

                }
                if (cerezsilasama == 3)
                {
                    System.Threading.Thread.Sleep(100);
                    tikla = PcProcess.yakingecmistemizle2;

                    if (ResimBul(tikla, "yakingecmistemizle2", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //Debug("Tikladim yakingecmistemizle2"); 
                    }

                }
                if (cerezsilasama == 4)
                {
                    System.Threading.Thread.Sleep(100);
                    tikla = PcProcess.gecmiszaman;

                    if (ResimBul(tikla, "gecmiszaman", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //Debug("Tikladim gecmiszaman"); 
                    }

                }
                if (cerezsilasama == 5)
                {
                    tikla = PcProcess.gecmishersey;

                    if (ResimBul(tikla, "gecmishersey", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //  Debug("Tikladim gecmishersey");
                    }

                }
                if (cerezsilasama == 6)
                {
                    tikla = PcProcess.gecmissitetercih;

                    if (ResimBul(tikla, "gecmissitetercih", 1000, 5))
                    {
                        cerezsilasama += 1;
                        // Debug("Tikladim gecmissitetercih"); 
                    }

                }
                if (cerezsilasama == 7)
                {
                    tikla = PcProcess.gecmiscevrimdisi;

                    if (ResimBul(tikla, "gecmiscevrimdisi", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //Debug("Tikladim gecmiscevrimdisi"); 
                    }

                }
                if (cerezsilasama == 8)
                {

                    tikla = PcProcess.tamam2;

                    if (ResimBul(tikla, "tamam2", 1000, 5))
                    {
                        cerezsilasama += 1;
                        //Debug("Tikladim tamam2"); 
                    }

                }

                if (cerezsilasama != 9) { throw new Exception("Çerezler Silinemedi"); }
                else
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug(ex.ToString(), ConsoleColor.Red);
                return false;
            }

            return true;
        }


        public bool vpnkur_1(int deneme)
        {


            try
            {
                driver.Navigate().GoToUrl("https://addons.mozilla.org/tr/firefox/addon/surfshark-vpn-proxy/?utm_source=addons.mozilla.org&utm_medium=referral&utm_content=search");
                IWebElement vpnyuklebuton = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/section[1]/div/header/div[4]/div/div/a"));
                vpnyuklebuton.Click();
            }
            catch (Exception ex)
            {
                Debug("Fonksiyon vpnkur1 Hata Deneme:" + deneme, ConsoleColor.Red);
                Debug("Hata:" + ex, ConsoleColor.Red);
                return false;
            }
            return true;
        }

        public string MailBaslangic(string funname, int beklemesuresi, int maxtekrarsayisi)
        {
            int ResimBulint = 1;
            string maildonen = "";
            while (ResimBulint <= maxtekrarsayisi && maildonen == "")
            {
                maildonen = MailCek();
                if (maildonen == "") { ResimBulint += 1; }
                else
                {
                    return maildonen;

                    // Debug("Tikladim " + funname);
                }
                if (ResimBulint > maxtekrarsayisi) { throw new Exception("Mail Bulunamadı " + funname); }
                System.Threading.Thread.Sleep(beklemesuresi);
            }
            return maildonen;
        }

        public bool ResimBul(Bitmap tikla, string funname, int beklemesuresi, int maxtekrarsayisi)
        {

            int ResimBulint = 1;
            bool ResimBulbool = false;
            while (ResimBulint <= maxtekrarsayisi && ResimBulbool == false)
            {
                ResimBulbool = ResimEkleFun(ResimBulint, tikla, funname);
                if (!ResimBulbool) { ResimBulint += 1; }
                else
                {
                    return true;

                    //Debug("Tikladim " + funname);
                }
                if (ResimBulint > maxtekrarsayisi) { throw new Exception("Görsel Bulunamadı " + funname); }
                System.Threading.Thread.Sleep(beklemesuresi);
            }
            return ResimBulbool;

        }


        public bool ResimEkleFun(int deneme, Bitmap tikla, string funname)
        {

            Bitmap ekrangoruntusu = PcProcess.pcProcess.GetScreenCapture();
            try
            {

                if (GorselBulTikla(ekrangoruntusu, tikla)) { System.Threading.Thread.Sleep(100); return true; }
                else
                {
                    throw new Exception("Error-");

                }

            }
            catch (Exception ex)
            {
                Debug("Fonksiyon " + funname + " Hata Deneme:" + deneme, ConsoleColor.Red);
                Debug("Hata:" + ex, ConsoleColor.Red);
                return false;
            }




            return true;


        }

        public bool ResimBulma(Bitmap tikla, string funname, int beklemesuresi, int maxtekrarsayisi)
        {

            int ResimBulint = 1;
            bool ResimBulbool = false;
            while (ResimBulint <= maxtekrarsayisi && ResimBulbool == false)
            {
                ResimBulbool = ResimEklemeFun(ResimBulint, tikla, funname);
                if (!ResimBulbool) { ResimBulint += 1; }
                else
                {
                    return true;

                    //Debug("Tikladim " + funname);
                }
                if (ResimBulint > maxtekrarsayisi)
                {
                    Debug("Görsel Bulunamadı:" + funname, ConsoleColor.Red);
                }
                System.Threading.Thread.Sleep(beklemesuresi);
            }
            return ResimBulbool;

        }

        public bool ResimEklemeFun(int deneme, Bitmap tikla, string funname)
        {

            Bitmap ekrangoruntusu = PcProcess.pcProcess.GetScreenCapture();
            try
            {

                if (GorselBulTikla(ekrangoruntusu, tikla)) { System.Threading.Thread.Sleep(100); return true; }


            }
            catch (Exception ex)
            {

                Debug("Fonksiyon " + funname + " Hata Deneme:" + deneme, ConsoleColor.Yellow);

                return false;
            }




            return true;


        }
        public bool vpnkur()
        {
            try
            {

                int vpnkurasama = 1;

                int vpnkurint_1 = 1;
                bool vpnkurbool_1 = false;
                while (vpnkurint_1 <= 5 && vpnkurbool_1 == false)
                {
                    vpnkurbool_1 = vpnkur_1(vpnkurint_1);
                    if (!vpnkurbool_1) { vpnkurint_1 += 1; }
                    else { vpnkurasama += 1; }
                    if (vpnkurint_1 > 5) { throw new Exception("Vpn Kur Buton Bulunamadı"); }
                    System.Threading.Thread.Sleep(1000);
                }





                Bitmap tikla = PcProcess.eklevpn;
                Bitmap ekrangoruntusu = PcProcess.pcProcess.GetScreenCapture();


                if (vpnkurasama == 2)
                {
                    if (ResimBul(tikla, "eklevpn", 1000, 5))
                    {
                        vpnkurasama += 1;
                        //Debug("Tikladim eklevpn"); 
                    }

                }


                if (vpnkurasama == 3)
                {
                    tikla = PcProcess.vpntamam;


                    if (ResimBul(tikla, "vpntamam", 1000, 5))
                    {
                        vpnkurasama += 1;
                        //Debug("Tikladim vpntamam"); 
                    }
                }

                if (vpnkurasama == 4)
                {
                    tikla = PcProcess.surfsharkac;


                    if (ResimBul(tikla, "surfsharkac", 1000, 5))
                    {
                        vpnkurasama += 1;
                        // Debug("Tikladim surfsharkac");
                    }
                }

                if (vpnkurasama == 5)
                {
                    tikla = PcProcess.suremail;


                    if (ResimBul(tikla, "suremail", 1000, 5))
                    {
                        vpnkurasama += 1;
                        //Debug("Email Girildi"); 
                        SendKeys.Send("surfsharkmailadresiniz");//kayıtlı mail adresiniz
                    }
                }

                if (vpnkurasama == 6)
                {
                    tikla = PcProcess.surpass;


                    if (ResimBul(tikla, "surpass", 1000, 5))
                    {
                        vpnkurasama += 1;
                        //Debug("şifre girildi");
                        SendKeys.Send("şifreniz");//şifreniz
                    }
                }
                if (vpnkurasama == 7)
                {
                    tikla = PcProcess.surlogin;


                    if (ResimBul(tikla, "surlogin", 1000, 10))
                    {
                        vpnkurasama += 1;
                        //Debug("Tikladim surlogin");
                    }
                }
                string randomulkesec = "";
                string aramaulke = "";

                if (vpnkurasama == 8)
                {
                    tikla = PcProcess.surlocations;

                    //randomulke
                    randomulkesec = randomulke();

                    sonulke = randomulkesec;

                    aramaulke = aramalistesi(sonulke);

                    Debug("Seçilen Ülke:" + sonulke, ConsoleColor.Gray);

                    if (ResimBul(tikla, "surlocations", 1000, 10))
                    {
                        vpnkurasama += 1;
                        // Debug("Tikladim surlocations"); 
                        SendKeys.Send(aramaulke); System.Threading.Thread.Sleep(1000);
                    }

                }

                if (vpnkurasama == 9)
                {
                    tikla = PcProcess.ulkeresmigetir(sonulke);


                    if (ResimBul(tikla, "surarananulke", 1000, 10))
                    {
                        vpnkurasama += 1;
                        // Debug("Tikladim surargentina");
                    }
                }

                if (vpnkurasama == 10)
                {
                    //baglandı vpn
                    tikla = PcProcess.survpnbaglandi;


                    if (ResimBul(tikla, "survpnbaglandi", 1000, 15))
                    {
                        vpnkurasama += 1;
                        //Debug("Tikladim survpnbaglandi"); 
                    }
                }
                if (vpnkurasama == 11)
                {
                   
                        vpnkurasama += 1;
                     

                }

                if (vpnkurasama == 12)
                {
                    //baglandı vpn
                    tikla = PcProcess.survpnpencerekapat;


                    if (ResimBul(tikla, "survpnpencerekapat", 1000, 15))
                    {
                        vpnkurasama += 1;
                        //Debug("Tikladim survpnpencerekapat"); 
                    }
                }

                if (vpnkurasama != 13)
                {
                    throw new Exception("Vpn Kurulamadı");
                }
                else
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                Debug("VPN Açılamadı " + ex.ToString(), ConsoleColor.Red);
                return false;
            }

            return true;


        }

        public string sonipadresi = "";

        public string ipcek(string funname, int beklemesuresi, int maxtekrarsayisi)
        {
            int ResimBulint = 1;
            string maildonen = "";
            while (ResimBulint <= maxtekrarsayisi && maildonen == "")
            {
                maildonen = ipcekfun();
                if (maildonen == "") { ResimBulint += 1; }
                else
                {
                    return maildonen;

                    Debug("İp Adresi: " + funname, ConsoleColor.Green);
                }
                if (ResimBulint > maxtekrarsayisi) { throw new Exception("Mail Bulunamadı " + funname); }

                System.Threading.Thread.Sleep(beklemesuresi);
            }
            return maildonen;



        }
        public string ipcekfun()
        {
            try
            {
                driver.Navigate().GoToUrl("https://surfshark.com/what-is-my-ip");

                IWebElement ipelement = driver.FindElement(By.XPath("/html/body/main/section[2]/div/div/div[2]/div[1]/span[2]"));
                string ipadresi = ipelement.GetAttribute("innerHTML");
                sonipadresi = ipadresi;
                Debug(sonipadresi + " İp'sine Bağlanıldı", ConsoleColor.Green);
                return sonipadresi;
            }
            catch (Exception ex)
            {
                Debug("(İpCek) Hata: ", ConsoleColor.Red);
                return "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        public string randomulke()
        {
            ArrayList ulkeisimleri = new ArrayList() {

"Arjantin",
"Arnavutluk",
"Avustralya - Adelaide",
"Avustralya - Melbourne",
"Avustralya - Perth",
"Avustralya - Brisbane",
"Avustralya - Sidney",
"Avusturya",
"Azerbaycan",
"Belçika - Antwerp",
"Belçika - Brüksel",
"Bosna ve Hersek",
"Brezilya",
"Bulgaristan",
"Kanada - Toronto",
"Kanada - Montreal",
"Kanada - Vancouver",
"Şili",
"Kolombiya",
"Kosta Rika",
"Hırvatistan",
"Kıbrıs Rum Kesimi",
"Çek Cumhuriyeti",
"Danimarka",
"Estonya",
"Finlandiya",
"Fransa - Marsilya",
"Fransa - Bordeaux",
"Fransa - Paris",
"Gürcistan",
"Yunanistan",
"Hong Kong",
"Macaristan",
"İzlanda",
"Hindistan - Mumbai",
"Hindistan - Chennai",
"Hindistan - Indore",
"Endonezya",
"İrlanda",
"İsrail",
"İtalya - Milano",
"İtalya - Roma",
"Japonya",
"Kazakistan",
"Letonya",
"Lüksemburg",
"Meksika",
"Moldova",
"Hollanda",
"Yeni Zelanda",
"Nijerya",
"Kuzey Makedonya",
"Norveç",
"Panama",
"Peru",
"Filipinler",
"Polonya - Gdansk",
"Polonya - Varşova",
"Portekiz - Porto",
"Portekiz - Lizbon",
"Romanya",
"Sırbistan",
"Singapur",
"Slovakya",
"Slovenya",
"Güney Afrika",
"Güney Kore",
"İspanya - Barselona",
"İspanya - Madrid",
"İspanya - Valencia",
"İsveç",
"İsviçre",
"Tayvan",
"Tayland",
"Türkiye",
"Ukrayna",
"Birleşik Arap Emirlikleri",
"İngiltere - Glasgow",
"İngiltere - Londra",
"İngiltere - Manchester",
"Birleşik Devletler - Charlotte",
"Birleşik Devletler - Miami",
"Birleşik Devletler - Seattle",
"Birleşik Devletler - Buffalo",
"Birleşik Devletler - Orlando",
"Birleşik Devletler - Detroit",
"Birleşik Devletler - Los Angeles",
"Birleşik Devletler - Latham",
"Birleşik Devletler - Ashburn",
"Birleşik Devletler - Denver",
"Birleşik Devletler - Atlanta",
"Birleşik Devletler - Tampa",
"Birleşik Devletler - Bend",
"Birleşik Devletler - San Francisco",
"Birleşik Devletler - Salt Lake City",
"Birleşik Devletler - Las Vegas",
"Birleşik Devletler - Phoenix",
"Birleşik Devletler - St. Louis",
"Birleşik Devletler - Dallas",
"Birleşik Devletler - Houston",
"Birleşik Devletler - Şikago",
"Birleşik Devletler - New York",
"Birleşik Devletler - Boston",
"Birleşik Devletler - Kansas City",
"Venezuela",

        };

        

            Random rndulke = new Random();

            int ulkernd = rndulke.Next(0, ulkeisimleri.Count);


            return ulkeisimleri[ulkernd].ToString();

        }
        public string aramalistesi(string ulkeismi)
        {
            string donenulke = "";
            if (ulkeismi == "Arjantin") { donenulke = "Arjantin"; }
            if (ulkeismi == "Arnavutluk") { donenulke = "Arnavutluk"; }
            if (ulkeismi == "Avustralya - Adelaide") { donenulke = "Adelaide"; }
            if (ulkeismi == "Avustralya - Melbourne") { donenulke = "Melbourne"; }
            if (ulkeismi == "Avustralya - Perth") { donenulke = "Perth"; }
            if (ulkeismi == "Avustralya - Brisbane") { donenulke = "Brisbane"; }
            if (ulkeismi == "Avustralya - Sidney") { donenulke = "Sidney"; }
            if (ulkeismi == "Avusturya") { donenulke = "Avusturya"; }
            if (ulkeismi == "Azerbaycan") { donenulke = "Azerbaycan"; }
            if (ulkeismi == "Belçika - Antwerp") { donenulke = "Antwerp"; }
            if (ulkeismi == "Belçika - Brüksel") { donenulke = "Brüksel"; }
            if (ulkeismi == "Bosna ve Hersek") { donenulke = "Bosna ve Hersek"; }
            if (ulkeismi == "Brezilya") { donenulke = "Brezilya"; }
            if (ulkeismi == "Bulgaristan") { donenulke = "Bulgaristan"; }
            if (ulkeismi == "Kanada - Toronto") { donenulke = "Toronto"; }
            if (ulkeismi == "Kanada - Montreal") { donenulke = "Montreal"; }
            if (ulkeismi == "Kanada - Vancouver") { donenulke = "Vancouver"; }
            if (ulkeismi == "Şili") { donenulke = "Şili"; }
            if (ulkeismi == "Kolombiya") { donenulke = "Kolombiya"; }
            if (ulkeismi == "Kosta Rika") { donenulke = "Kosta Rika"; }
            if (ulkeismi == "Hırvatistan") { donenulke = "Hırvatistan"; }
            if (ulkeismi == "Kıbrıs Rum Kesimi") { donenulke = "Kıbrıs Rum Kesimi"; }
            if (ulkeismi == "Çek Cumhuriyeti") { donenulke = "Çek Cumhuriyeti"; }
            if (ulkeismi == "Danimarka") { donenulke = "Danimarka"; }
            if (ulkeismi == "Estonya") { donenulke = "Estonya"; }
            if (ulkeismi == "Finlandiya") { donenulke = "Finlandiya"; }
            if (ulkeismi == "Fransa - Marsilya") { donenulke = "Marsilya"; }
            if (ulkeismi == "Fransa - Bordeaux") { donenulke = "Bordeaux"; }
            if (ulkeismi == "Fransa - Paris") { donenulke = "Paris"; }
            if (ulkeismi == "Gürcistan") { donenulke = "Gürcistan"; }
            if (ulkeismi == "Yunanistan") { donenulke = "Yunanistan"; }
            if (ulkeismi == "Hong Kong") { donenulke = "Hong Kong"; }
            if (ulkeismi == "Macaristan") { donenulke = "Macaristan"; }
            if (ulkeismi == "İzlanda") { donenulke = "İzlanda"; }
            if (ulkeismi == "Hindistan - Mumbai") { donenulke = "Mumbai"; }
            if (ulkeismi == "Hindistan - Chennai") { donenulke = "Chennai"; }
            if (ulkeismi == "Hindistan - Indore") { donenulke = "Indore"; }
            if (ulkeismi == "Endonezya") { donenulke = "Endonezya"; }
            if (ulkeismi == "İrlanda") { donenulke = "İrlanda"; }
            if (ulkeismi == "İsrail") { donenulke = "İsrail"; }
            if (ulkeismi == "İtalya - Milano") { donenulke = "Milano"; }
            if (ulkeismi == "İtalya - Roma") { donenulke = "Roma"; }
            if (ulkeismi == "Japonya") { donenulke = "Japonya"; }
            if (ulkeismi == "Kazakistan") { donenulke = "Kazakistan"; }
            if (ulkeismi == "Letonya") { donenulke = "Letonya"; }
            if (ulkeismi == "Lüksemburg") { donenulke = "Lüksemburg"; }
            if (ulkeismi == "Meksika") { donenulke = "Meksika"; }
            if (ulkeismi == "Moldova") { donenulke = "Moldova"; }
            if (ulkeismi == "Hollanda") { donenulke = "Hollanda"; }
            if (ulkeismi == "Yeni Zelanda") { donenulke = "Yeni Zelanda"; }
            if (ulkeismi == "Nijerya") { donenulke = "Nijerya"; }
            if (ulkeismi == "Kuzey Makedonya") { donenulke = "Kuzey Makedonya"; }
            if (ulkeismi == "Norveç") { donenulke = "Norveç"; }
            if (ulkeismi == "Panama") { donenulke = "Panama"; }
            if (ulkeismi == "Peru") { donenulke = "Peru"; }
            if (ulkeismi == "Filipinler") { donenulke = "Filipinler"; }
            if (ulkeismi == "Polonya - Gdansk") { donenulke = "Gdansk"; }
            if (ulkeismi == "Polonya - Varşova") { donenulke = "Varşova"; }
            if (ulkeismi == "Portekiz - Porto") { donenulke = "Porto"; }
            if (ulkeismi == "Portekiz - Lizbon") { donenulke = "Lizbon"; }
            if (ulkeismi == "Romanya") { donenulke = "Romanya"; }
            if (ulkeismi == "Sırbistan") { donenulke = "Sırbistan"; }
            if (ulkeismi == "Singapur") { donenulke = "Singapur"; }
            if (ulkeismi == "Slovakya") { donenulke = "Slovakya"; }
            if (ulkeismi == "Slovenya") { donenulke = "Slovenya"; }
            if (ulkeismi == "Güney Afrika") { donenulke = "Güney Afrika"; }
            if (ulkeismi == "Güney Kore") { donenulke = "Güney Kore"; }
            if (ulkeismi == "İspanya - Barselona") { donenulke = "Barselona"; }
            if (ulkeismi == "İspanya - Madrid") { donenulke = "Madrid"; }
            if (ulkeismi == "İspanya - Valencia") { donenulke = "Valencia"; }
            if (ulkeismi == "İsveç") { donenulke = "İsveç"; }
            if (ulkeismi == "İsviçre") { donenulke = "İsviçre"; }
            if (ulkeismi == "Tayvan") { donenulke = "Tayvan"; }
            if (ulkeismi == "Tayland") { donenulke = "Tayland"; }
            if (ulkeismi == "Türkiye") { donenulke = "Türkiye"; }
            if (ulkeismi == "Ukrayna") { donenulke = "Ukrayna"; }
            if (ulkeismi == "Birleşik Arap Emirlikleri") { donenulke = "Birleşik Arap Emirlikleri"; }
            if (ulkeismi == "İngiltere - Glasgow") { donenulke = "Glasgow"; }
            if (ulkeismi == "İngiltere - Londra") { donenulke = "Londra"; }
            if (ulkeismi == "İngiltere - Manchester") { donenulke = "Manchester"; }
            if (ulkeismi == "Birleşik Devletler - Charlotte") { donenulke = "Charlotte"; }
            if (ulkeismi == "Birleşik Devletler - Miami") { donenulke = "Miami"; }
            if (ulkeismi == "Birleşik Devletler - Seattle") { donenulke = "Seattle"; }
            if (ulkeismi == "Birleşik Devletler - Buffalo") { donenulke = "Buffalo"; }
            if (ulkeismi == "Birleşik Devletler - Orlando") { donenulke = "Orlando"; }
            if (ulkeismi == "Birleşik Devletler - Detroit") { donenulke = "Detroit"; }
            if (ulkeismi == "Birleşik Devletler - Los Angeles") { donenulke = "Los Angeles"; }
            if (ulkeismi == "Birleşik Devletler - Latham") { donenulke = "Latham"; }
            if (ulkeismi == "Birleşik Devletler - Ashburn") { donenulke = "Ashburn"; }
            if (ulkeismi == "Birleşik Devletler - Denver") { donenulke = "Denver"; }
            if (ulkeismi == "Birleşik Devletler - Atlanta") { donenulke = "Atlanta"; }
            if (ulkeismi == "Birleşik Devletler - Tampa") { donenulke = "Tampa"; }
            if (ulkeismi == "Birleşik Devletler - Bend") { donenulke = "Bend"; }
            if (ulkeismi == "Birleşik Devletler - San Francisco") { donenulke = "San Francisco"; }
            if (ulkeismi == "Birleşik Devletler - Salt Lake City") { donenulke = "Salt Lake City"; }
            if (ulkeismi == "Birleşik Devletler - Las Vegas") { donenulke = "Las Vegas"; }
            if (ulkeismi == "Birleşik Devletler - Phoenix") { donenulke = "Phoenix"; }
            if (ulkeismi == "Birleşik Devletler - St. Louis") { donenulke = "St. Louis"; }
            if (ulkeismi == "Birleşik Devletler - Dallas") { donenulke = "Dallas"; }
            if (ulkeismi == "Birleşik Devletler - Houston") { donenulke = "Houston"; }
            if (ulkeismi == "Birleşik Devletler - Şikago") { donenulke = "Şikago"; }
            if (ulkeismi == "Birleşik Devletler - New York") { donenulke = "New York"; }
            if (ulkeismi == "Birleşik Devletler - Boston") { donenulke = "Boston"; }
            if (ulkeismi == "Birleşik Devletler - Kansas City") { donenulke = "Kansas City"; }
            if (ulkeismi == "Venezuela") { donenulke = "Venezuela"; }

            return donenulke;
        }
    }
}
