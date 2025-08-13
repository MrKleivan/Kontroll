const Models = {
    Transaction: {
        date: { name: "date", value: null }, // ISO-dato string, evt. Date-objekt
        userDescription: { name: "Beskrivelse", value: null },
        amount: { name: "Beløp", value: 0 },
        othersBankAccount: { name: "Motparts konto nr", value: null },
        supplierId: { name: "supplierId", value: null },
        isFixedExpense: { name: "Er fast utgift", value: false },
        fixedTransactionId: { name: "fixedTransactionId", value: null },
        hasReceipt: { name: "Kvittering", value: false },
        hasInvoice: { name: "Faktura", value: false },
    },

}

export { Models };