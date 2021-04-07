using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Energialaskuriproto
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Hae_click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Parse(Request.Form[pvmr.UniqueID]);
            

            string paivamaara = (dt.ToString("yyyy-MM-dd"));

            paivamaara.InnerHtml = dt.ToString("dd/MM/yyyy" + "<br>");

            // Tuulivoima
            foreach (ListItem li in Checkboxit.Items)
            {
               
                // If the list item is selected
                if (li.Selected)
                {
                    tuuliv.Controls.Add(new Label() { Text = li.Text });
                    tuuliv.Controls.Add(new Label() { Text = " X MWh" });
                    tuuliv.Controls.Add(new Label() { Text = "<br><br>" });
                }
            }

            // Ydinvoima
            foreach (ListItem li in Checkboxit2.Items)
            {
                // If the list item is selected
                if (li.Selected)
                {
                    tuuliv.Controls.Add(new Label() { Text = li.Text });
                    tuuliv.Controls.Add(new Label() { Text = " X MWh" });
                    tuuliv.Controls.Add(new Label() { Text = "<br><br>" });
                }
            }

            // Aurinkovoima
            foreach (ListItem li in Checkboxit3.Items)
            {
                // If the list item is selected
                if (li.Selected)
                {
                    // LÄHETÄ FINNGRIDILLE X-API-KEY JA HAETTAVAT KOHTEET----------------------------------------------------------------------------------------------//
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/247/events/xml?start_time=2021-02-23T00%3A00%3A00Z&end_time=2021-02-23T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //-------------------------------------------------------------------------------------------------------------------------------------------------//
                    // Hae tieto ja splittaa se
                    //Response.Write(responseFromServer);                                          // DEBUG    TULOSTA KAIKKI TIETO ID:LTÄ
                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 // REGEX:llä koko sanan avulla splittaaminen
                    int i = 0;
                    int endPoint = 0;
                    //int alkioidenMaara = 0;                                                       // DEBUG    Alkioidenmäärän alustus tulostuksen toimivuuteen
                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           // Stringin siivoamista

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     // Tyhjän alkion ohittaminen
                        {
                            //------------------------------------------------------------------------------------------------------------------------------------------//
                            NumberFormatInfo provider = new NumberFormatInfo();                     // Alkion desimaalin formatointi doubleen sopivaksi 
                            provider.NumberDecimalSeparator = ".";                                  // Ei jostain syystä lähtenyt toimimaan tryparsella
                            provider.NumberGroupSeparator = ",";                                    // Menen siis näin
                            double temp = Convert.ToDouble(haetieto[i], provider);                  //
                                                                                                    //------------------------------------------------------------------------------------------------------------------------------------------//

                             temp = temp / 1;                                             // Vaihteluväli haettu nappulan määrityksistä
                             tuotanto += temp;

                            //Response.Write(haetieto[i] + "\n");                                          // DEBUG    Näytetään kaikki tiedot ja alkiot
                        }

                        i++;

                        //Response.Write("<br/>i: " + i + ". Tuotanto: ");                          // DEBUG    Näytetään kaikki tiedot ja alkiot
                        //alkioidenMaara = i - 1;                                                   // DEBUG    Alkioihin

                    }

                    //Response.Write("<br/><br/>Alkioiden määrä: " + alkioidenMaara);               // DEBUG    Tulosta alkiot
                    //Response.Write("<br/>"+id+": Kokonaistuotanto: " + tuotanto);                 // DEBUG    Tulosta kokonaistuotanto alkioista
                    //Response.Write(tuotanto + "MW");


                    output1.InnerHtml = "Aurinkovoima <br>" + tuotanto + " MWh";
                    //tuuliv.Controls.Add(new Label() { Text = li.Text });
                    //tuuliv.Controls.Add(new Label() { Text = " X MWh" });
                    //tuuliv.Controls.Add(new Label() { Text = "<br><br>" });
                }
            }
            
            // Vesivoima
            foreach (ListItem li in Checkboxit4.Items)
            {
                // If the list item is selected
                if (li.Selected)
                {

                    // LÄHETÄ FINNGRIDILLE X-API-KEY JA HAETTAVAT KOHTEET----------------------------------------------------------------------------------------------//
                    WebRequest myRequest = WebRequest.Create(@"https://api.fingrid.fi/v1/variable/191/events/xml?start_time=2021-02-23T00%3A00%3A00Z&end_time=2021-02-23T23%3A59%3A59Z");
                    string usernamePassword = "8fhjOkXWl58ZrgbFaEDVN7vZ7s0Js9T91Lo8M1ZT";
                    myRequest.Headers.Add("x-api-key", usernamePassword);
                    WebResponse response = myRequest.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    //-------------------------------------------------------------------------------------------------------------------------------------------------//
                    // Hae tieto ja splittaa se
                    //Response.Write(responseFromServer);                                          // DEBUG    TULOSTA KAIKKI TIETO ID:LTÄ
                    string[] haetieto = Regex.Split(responseFromServer, "<event>");                 // REGEX:llä koko sanan avulla splittaaminen
                    int i = 0;
                    int endPoint = 0;
                    //int alkioidenMaara = 0;                                                       // DEBUG    Alkioidenmäärän alustus tulostuksen toimivuuteen
                    double tuotanto = 0;
                    while (i < haetieto.Length)
                    {
                        if (haetieto[i].Contains("<value>"))
                        {
                            endPoint = haetieto[i].IndexOf("</value>");
                        }
                        haetieto[i] = haetieto[i].Substring(0, endPoint);
                        haetieto[i] = haetieto[i].Replace("<value>", "");                           // Stringin siivoamista

                        if (!String.IsNullOrEmpty(haetieto[i]))                                     // Tyhjän alkion ohittaminen
                        {
                            //------------------------------------------------------------------------------------------------------------------------------------------//
                            NumberFormatInfo provider = new NumberFormatInfo();                     // Alkion desimaalin formatointi doubleen sopivaksi 
                            provider.NumberDecimalSeparator = ".";                                  // Ei jostain syystä lähtenyt toimimaan tryparsella
                            provider.NumberGroupSeparator = ",";                                    // Menen siis näin
                            double temp = Convert.ToDouble(haetieto[i], provider);                  //
                                                                                                    //------------------------------------------------------------------------------------------------------------------------------------------//

                            temp = temp / 20;                                             // Vaihteluväli haettu nappulan määrityksistä
                            tuotanto += temp;

                            //Response.Write(haetieto[i] + "\n");                                          // DEBUG    Näytetään kaikki tiedot ja alkiot
                        }

                        i++;

                        //Response.Write("<br/>i: " + i + ". Tuotanto: ");                          // DEBUG    Näytetään kaikki tiedot ja alkiot
                        //alkioidenMaara = i - 1;                                                   // DEBUG    Alkioihin

                    }

                    //Response.Write("<br/><br/>Alkioiden määrä: " + alkioidenMaara);               // DEBUG    Tulosta alkiot
                    //Response.Write("<br/>"+id+": Kokonaistuotanto: " + tuotanto);                 // DEBUG    Tulosta kokonaistuotanto alkioista
                    //Response.Write(tuotanto + "MW");


                    output2.InnerHtml = "Vesivoima <br>" + tuotanto + " MWh";

                }
            }
            
            // Kaukolämpö
            foreach (ListItem li in Checkboxit5.Items)
            {
                // If the list item is selected
                if (li.Selected)
                {
                    output3.InnerHtml = "Kaukolämpö: Dataa ei saatavilla.";
                }
            }


        }
    }
 }
    
