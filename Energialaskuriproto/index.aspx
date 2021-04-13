<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Energialaskuriproto.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
   <link rel="stylesheet" href="style.css">
    <title>Document</title>
</head>
<body>

<form id="form2" runat="server">
 
    <div class="ruutu">
        
        <div class="header">
            <h1>energialaskuri</h1>
            <img height="80px" src="harmaa.png" />
        </div>

        <div class="menu">
            <ul>
                <li><a href="etusivu.aspx">Etusivu</a></li>
                <li class="active"><a href="index.html">Data</a></li>
                <li><a href="ohje.aspx">Käyttöohje</a></li>
            </ul>
        </div>

        <div class="pvmr">
            
            <div class="date">
                <label for="pvmm">Päivämäärä:</label>
                <input type="date" id="pvmr" name="pvmm" runat="server" value="2021-03-30"/>
            </div>

        </div>

        <div class="pohja">
            
            <div class="col1">
                 
                <div class="hae">                  
                    <!--<p>►</p>-->
                    <asp:Button Text="Hae ►" runat="server" OnClick="Hae_click"  repeatDirection="Vertical"/>
                      
                    </asp:Button>
                    <!--<img src="kolmio.png" />-->
                </div>
                
               <!--Tuulivoima-->
                <div class="nappi">
                    <asp:CheckBoxList ID="Checkboxit" runat="server">
                        <asp:ListItem  Text="  Tuulivoima"> </asp:ListItem>                        
                   </asp:CheckBoxList>  
                 </div>
                <!--Ydinvoima-->
                <div class="nappi">
                    <asp:CheckBoxList ID="Checkboxit2" runat="server">                       
                       <asp:ListItem  Text="  Ydinvoima"> </asp:ListItem>                        
                   </asp:CheckBoxList>                  
                </div>
                <!--Aurinkovoima-->
                <div class="nappi">
                    <asp:CheckBoxList ID="Checkboxit3" runat="server">                        
                       <asp:ListItem  Text="  Aurinkovoima"> </asp:ListItem>                        
                   </asp:CheckBoxList> 
                </div>
                <!--Vesivoima-->
                <div class="nappi">
                    <asp:CheckBoxList ID="Checkboxit4" runat="server">                        
                       <asp:ListItem  Text="  Vesivoima"> </asp:ListItem>                        
                   </asp:CheckBoxList>
                </div>
                <!--Kaukolämpö-->
                <div class="nappi">
                    <asp:CheckBoxList ID="Checkboxit5" runat="server">                        
                       <asp:ListItem  Text="  Kaukolämpö"> </asp:ListItem>                        
                   </asp:CheckBoxList>
                </div>

                

            
            </div>
            
            <div class="col2"> 
                
                <div class="box" id="tuuliv" runat="server">
                    <h3>Tulos: </h3>
                    <h3 id="pvm" runat="server"> </h3>
                    <p runat="server" id="output1"></p>
                    <p runat="server" id="output2"></p>
                    <p runat="server" id="output3"></p>
                    <p runat="server" id="output4"></p>
                    <p runat="server" id="output5"></p>
                </div>

                <div class="box" id="Div1" runat="server">
                    <!--<p>Osuus kokonaistuotannosta: </p>-->
                    <p id="kokoTuotanto" runat="server"> </p>
                    <br />
                    <p id="osuusTuotanto" runat="server"> </p>
                    
                </div>

            </div>
        
        </div>
        <p style="text-align: center;"> © Sankarit</p>
    </div>
    


</form>
  <!--  
<script>


Date.prototype.toDateInputValue = (function () {
var local = new Date(this);
local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
return local.toJSON().slice(0, 10);
});

 document.getElementById('pvmr').value = new Date().toDateInputValue();
</script> 
 -->
</body>
</html>



