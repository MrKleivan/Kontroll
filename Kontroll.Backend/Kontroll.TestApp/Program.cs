using Kontroll.Controller;
using Kontroll.Database;
using Kontroll.TestApp;
using Kontroll.TestApp.DescriptionTests;
using Kontroll.TestApp.FixedExpenseTests;
using Kontroll.TestApp.SupplierTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// test om jeg fikk lagt inn lest fra exel og gjort om til objekt: TransactionOB og lagt inn i database og om en transaksjon me samme dato og beløp eksisterer
// skal jeg få en melding at det eksisterer en lik transaksjon og jeg skal få et spørsmål om jeg fortsatt vil legge til hvor jeg må svare ja eller nei,
// om ja skal det ligge seg et duplikat av denne med et annet TransactionId
// sist testet og vellykket 25.04.25
var TransactionTestOne = new TransactionTestOne(); 

// test om jeg får tilbake riktig transaction ved å sende en get forespørsel basert på transactionId
// sist testet og vellykket 25.04.25
var TransactionTestTwo = new TransactionTestTwo(); 

// test på å sortere transaction på dato og en test hvor jeg sorterer på beløp både innkommende beløp og utkommende
// sist testet og vellykket 25.04.25
var TransactionTestThree = new TransactionTestThree(); 

// test på oppdatere en eksisterende transaksjon hvor jeg endrer på utkommende beløp her laster den opp transaksjonen førs og viser utkommende beløp som allerede er registrert
// så plusser den på en og oppdaterer beløpet til transaksjonen i databasen og sender ny spårring og viser den nåverende beløpet
// sist testet og vellykket 25.04.25
var TransactionTestFoure = new TransactionTestFoure();

// her tester jeg å legge til en ny transaksjon hvor jeg har harkodet verdiene
// sist testet og vellykket 25.04.25
var TransactionTestFive = new TransactionTestFive();

// test hvor jeg legger til en ny description i DescriptionTB i databasen
// sist testet og vellykket 25.04.25
var DescriptionTestOne = new DescriptionTestOne();

// test hvor jeg legger til en ny transaksjon som har en ExternalDescription som finnes i database tabellen med Description objekter
// og legger da til UserDescription verdien fra Description objektet til transaction objektet automatins før transaksjonen blir lagt til i tabellen i databasen
// sist testet og vellykket 25.04.25
var DescriptionTestTwo = new DescriptionTestTwo();

// 
var DescriptionTestThree = new DescriptionTestThree();


var FixedExpenseTestOne = new FixedExpenseTestOne();


var FixedExpenseTestTwo = new FixedExpenseTestTwo();


var SupplierTestOne = new SupplierTestOne();


await TransactionTestOne.Run(config);
