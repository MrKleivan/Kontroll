using Kontroll.Controller;
using Kontroll.Database;
using Kontroll.TestApp;
using Kontroll.TestApp.DescriptionTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// testet om jeg fikk lagt inn lest fra exel og gjort om til objekt: TransactionOB og lagt inn i database og om en transaksjon me samme dato og beløp eksisterer
// skal jeg få en melding at det eksisterer en lik transaksjon og jeg skal få et spørsmål om jeg fortsatt vil legge til hvor jeg må svare ja eller nei,
// om ja skal det ligge seg et duplikat av denne med et annet TransactionId
var testOne = new TransactionTestOne(); 

// testet om jeg får tilbake riktig transaction ved å sende en get forespørsel basert på transactionId
var TransactionTestTwo = new TransactionTestTwo(); 

// testet på å sortere transactin på dato og en tet hvor jeg sorterer på beløp både innkommende beløp og utkommende
var TransactionTestThree = new TransactionTestThree(); 

// test på oppdatere en eksisterende transaksjon hvor jeg endrer på utkommende beløp her laster den opp transaksjonen førs og viser utkommende beløp som allerede er registrert
// så plusser den på en og oppdaterer beløpet til transaksjonen i databasen og sender ny spårring og viser den nåverende beløpet
var TransactionTestFoure = new TransactionTestFoure();

// her tester jeg å legge til en ny transaksjon hvor jeg har harkodet verdiene
var TransactionTestFive = new TransactionTestFive();


var DescriptionTestOne = new DescriptionTestOne();


var DescriptionTestTwo = new DescriptionTestTwo();

await DescriptionTestTwo.Run(config);
