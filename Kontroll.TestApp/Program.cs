using Kontroll.Controller;
using Kontroll.Database;
using Kontroll.TestApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// testet om jeg fikk lagt inn lest fra exel og gjort om til objekt: TransactionOB og lagt inn i database og om en transaksjon me samme dato og beløp eksisterer
// skal jeg få en melding at det eksisterer en lik transaksjon og jeg skal få et spørsmål om jeg fortsatt vil legge til hvor jeg må svare ja eller nei,
// om ja skal det ligge seg et duplikat av denne med et annet TransactionId
var testOne = new TestOne(); 

// testet om jeg får tilbake riktig transaction ved å sende en get forespørsel basert på transactionId
var testTwo = new TestTwo(); 

// testet på å sortere transactin på beløp både innkommende beløp og utkommende
var testThree = new TestThree(); 

await testOne.Run(config);