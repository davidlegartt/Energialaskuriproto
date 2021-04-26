using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


namespace Energialaskuriproto
{
    public class Global
    {
        public static double ktuotanto;
        public static double tuuliOsuus;
        public static double ydinOsuus;
        public static double aurinkoOsuus;
        public static double vesiOsuus;
        public static double kaukoOsuus;

        public static double kaikkiOsuus;
    }
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Div1.Visible = false;
        }

        protected void Hae_click(object sender, EventArgs e)
        {
            Div1.Visible = true;

            string paiva = "";
            string kuukausi = "";
            string vuosi = "";
            string paivamaara = "";
            string paivamaara1 = "";
            DateTime dt = DateTime.Now;
            


            
            try
            { 
            dt = DateTime.Parse(Request.Form[pvmr.UniqueID]);
            paivamaara = (dt.ToString("yyyy-MM-dd"));
            paivamaara1 = (dt.ToString("dd.MM.yyyy"));

            pvm.InnerHtml = dt.ToString("dd/MM/yyyy" + "<br>");
            }
            catch
            {
            paivamaara = DateTime.Now.ToString("yyyy-MM-dd");
            paivamaara1 = DateTime.Now.ToString("dd.MM.yyyy");
            pvm.InnerHtml = dt.ToString("dd/MM/yyyy" + "<br>");

            }
            paiva = dt.ToString("dd");
            kuukausi = dt.ToString("MM");
            vuosi = dt.ToString("yyyy");


            //KOKONAISTUOTANTO
            int x = 1;
            if (x == 1)
            {
                WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/74/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                myRequest.Headers.Add("x-api-key", usernamePassword);
                WebResponse response = myRequest.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                int i = 0;
                int endPoint = 0;
                Global.ktuotanto = 0;
                while (i < haetieto.Length)
                {
                    if (haetieto[i].Contains("<value>"))
                    {
                        endPoint = haetieto[i].IndexOf("</value>");
                    }
                    haetieto[i] = haetieto[i].Substring(0, endPoint);
                    haetieto[i] = haetieto[i].Replace("<value>", "");                           

                    if (!String.IsNullOrEmpty(haetieto[i]))                                     
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();                      
                        provider.NumberDecimalSeparator = ".";                                  
                        provider.NumberGroupSeparator = ",";                                    
                        double temp = Convert.ToDouble(haetieto[i], provider);                  
                                                                                                
                        temp = temp / 1;                                             
                        Global.ktuotanto += temp;
                    }
                    i++;
                }
                kokoTuotanto.Controls.Add(new Label() { Text = "Kokonaistuotanto: <br>" + Math.Round(Global.ktuotanto, 2) + " MWh" });
            }
            
            // Tuulivoima
            foreach (ListItem li in Checkboxit.Items)
            {
                if (li.Selected)
                {
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/75/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                    int i = 0;
                    int endPoint = 0;                                                     
                    double tuotanto = 0;
                    
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     
                        {
                            NumberFormatInfo provider = new NumberFormatInfo();                      
                            provider.NumberDecimalSeparator = ".";                                
                            provider.NumberGroupSeparator = ",";                                    
                            double temp = Convert.ToDouble(haetieto[i], provider);                 
                                                                                                    
                            temp = temp / 1;                                             
                            tuotanto += temp;
                        }
                        
                        i++;

                    }
                    Global.tuuliOsuus = tuotanto / Global.ktuotanto * 100;
                    output1.Controls.Add(new Label() { Text = "Tuulivoima: <br>" + Math.Round(tuotanto, 2) + " MWh" });
                }
                else
                {
                    Global.tuuliOsuus = 0;
                }
            }

            // Ydinvoima
            foreach (ListItem li in Checkboxit2.Items)
            { 
                if (li.Selected)
                {
                    
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/188/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                    int i = 0;
                    int endPoint = 0;
                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     
                        {
                            NumberFormatInfo provider = new NumberFormatInfo();                      
                            provider.NumberDecimalSeparator = ".";                                  
                            provider.NumberGroupSeparator = ",";                                    
                            double temp = Convert.ToDouble(haetieto[i], provider);                  
                                                                                                    
                            temp = temp / 20;                                             
                            tuotanto += temp;
                        }

                        i++;

                    }
                    Global.ydinOsuus = tuotanto / Global.ktuotanto * 100;
                    output2.Controls.Add(new Label() { Text = "Ydinvoima: <br>" + Math.Round(tuotanto, 2) + " MWh" });
                }
                else
                {
                    Global.ydinOsuus = 0;
                }
            }

            // Aurinkovoima
            foreach (ListItem li in Checkboxit3.Items)
            {
                if (li.Selected)
                {   
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/247/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                    int i = 0;
                    int endPoint = 0;
                                                       
                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     
                        {
                            NumberFormatInfo provider = new NumberFormatInfo();                     
                            provider.NumberDecimalSeparator = ".";                                  
                            provider.NumberGroupSeparator = ",";                                    
                            double temp = Convert.ToDouble(haetieto[i], provider);                  

                            temp = temp / 1;                                             
                            tuotanto += temp;
                        }

                        i++;

                    }
                    Global.aurinkoOsuus = tuotanto / Global.ktuotanto * 100;
                    output3.Controls.Add(new Label() { Text = "Aurinkovoima: <br>" + Math.Round(tuotanto, 2) + " MWh" });
                }
                else
                {
                    Global.aurinkoOsuus = 0;
                }
            }
            
            // Vesivoima
            foreach (ListItem li in Checkboxit4.Items)
            {
                if (li.Selected)
                {   
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/191/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                    int i = 0;
                    int endPoint = 0;

                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     
                        {
                            NumberFormatInfo provider = new NumberFormatInfo();                     
                            provider.NumberDecimalSeparator = ".";                                  
                            provider.NumberGroupSeparator = ",";                                    
                            double temp = Convert.ToDouble(haetieto[i], provider);                                                                                       

                            temp = temp / 20;                                             
                            tuotanto += temp;
                        }

                        i++;

                    }
                    Global.vesiOsuus = tuotanto / Global.ktuotanto * 100;
                    output4.Controls.Add(new Label() { Text = "Vesivoima: <br>" + Math.Round(tuotanto, 2) + " MWh" });
                }
                else
                {
                    Global.vesiOsuus = 0;
                }
            }
            
            // Kaukolämpö
            foreach (ListItem li in Checkboxit5.Items)
            {
                if (li.Selected)
                {
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/201/events/xml?start_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T00%3A00%3A00Z&end_time=" + vuosi + "-" + kuukausi + "-" + paiva + "T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 
                    int i = 0;
                    int endPoint = 0;
                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     
                        {
                            NumberFormatInfo provider = new NumberFormatInfo();                     
                            provider.NumberDecimalSeparator = ".";                                  
                            provider.NumberGroupSeparator = ",";                                    
                            double temp = Convert.ToDouble(haetieto[i], provider);                  
                                                                                                    
                            temp = temp / 20;                                             
                            tuotanto += temp;                                          
                        }

                        i++;

                    }
                    Global.kaukoOsuus = tuotanto / Global.ktuotanto * 100;
                    output5.Controls.Add(new Label() { Text = "Kaukolämpö: <br>" + Math.Round(tuotanto, 2) + " MWh" });
                }
                else
                {
                    Global.kaukoOsuus = 0;
                }
            }
            
            Global.kaikkiOsuus = Global.tuuliOsuus + Global.ydinOsuus + Global.vesiOsuus + Global.aurinkoOsuus + Global.kaukoOsuus;
            if (Global.kaikkiOsuus > 0) {
                osuusTuotanto.Controls.Add(new Label() { Text = "Valittujen voimien osuus energian kokonaistuotannosta: <br>" + Math.Round(Global.kaikkiOsuus, 1) + "%" });
            }
        }
    }
 }





