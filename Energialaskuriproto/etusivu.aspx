<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ohje.aspx.cs" Inherits="Energialaskuriproto.ohje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="style.css">
    <title>Document</title>
</head>
<body>
    <div class="ruutu">
        
        <div class="header">
            <h1>energialaskuri</h1>
            <img height="80px" src="harmaa.png" />
        </div>

        <div class="menu">
            <ul>
                <li class="active"><a href="etusivu.aspx">Etusivu</a></li>
                <li><a href="index.aspx">Data</a></li>
                <li><a href="ohje.aspx">Käyttöohje</a></li>
            </ul>
        </div>


        <div class="pohja2">
            
            <h3>Energialaskuri hyödyntää laskuissaan Fingridin avointa dataa </h3>
            <br />
            <p>Energialaskurin tavoitteena on saada tietoa sähkön tuotannon jakautumisesta eri tuotantolähteiden välillä.</p>
            <br />
            <p>Sovellus tarjoaa helpon tavan tarkastella energiatuotannon määriä.</p>
            <br />
            <p>Sovellus on toistaiseksi kehityksessä ja on suunniteltu iPhone X:n mittojen mukaan.</p>


        
        </div>
        <div class="teksti">
            <p>Tutustu fingridiin painamalla logoa!</p>
        </div>
        
        <div class="logo">
            
            <a href="https://www.fingrid.fi/">
                   <img src="Logo.png"/>
            </a>
        </div>
        
        
        
    </div>
</body>
</html>