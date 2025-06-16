import Account from "./Account.vue";


const MyLinks = {
    HomePageLinks: [
        {name: 'home', label: 'Hjem'},
        {name: 'HomeSearch', label: 'Søk', svg: `<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="m15.75 15.75-2.489-2.489m0 0a3.375 3.375 0 1 0-4.773-4.773 3.375 3.375 0 0 0 4.774 4.774ZM21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" /></svg>`},
        {name: 'About', label: 'Om oss', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z" /></svg>'},
        {name: 'UserHome', label: 'Login', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M8.25 9V5.25A2.25 2.25 0 0 1 10.5 3h6a2.25 2.25 0 0 1 2.25 2.25v13.5A2.25 2.25 0 0 1 16.5 21h-6a2.25 2.25 0 0 1-2.25-2.25V15M12 9l3 3m0 0-3 3m3-3H2.25" /></svg>'},
    ],
    Main: [
        {name: 'Economy', label: 'Økonomi'},
        {name: 'Property', label: 'Eiendom'},
    ],
    Economy: [
        { name: 'Transaction', label: 'Transaksjoner', info: '- Overføringer/betalinger'},
        { name: 'FixedExpense', label: 'Faste Utgifter', info: '- Dine fastsatte utgifter'},
        { name: 'Suppliers', label: 'Leverandører', info: '- Leverandør oversikt'},
    ],

};

const Links = {
    HomePageHeader: {
        Links: [
            {name: 'home', label: 'Hjem'},
            {name: 'About', label: 'Om oss'},
            {name: 'HomeSearch', label: 'Søk', svg: `<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="m15.75 15.75-2.489-2.489m0 0a3.375 3.375 0 1 0-4.773-4.773 3.375 3.375 0 0 0 4.774 4.774ZM21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" /></svg>`},
            {name: 'About', label: 'Om oss', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z" /></svg>'},
            {name: 'UserHome', label: 'Login', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M8.25 9V5.25A2.25 2.25 0 0 1 10.5 3h6a2.25 2.25 0 0 1 2.25 2.25v13.5A2.25 2.25 0 0 1 16.5 21h-6a2.25 2.25 0 0 1-2.25-2.25V15M12 9l3 3m0 0-3 3m3-3H2.25" /></svg>'},
        ]
    },
    UserHomePageHeader: {
        Links: [
            {name: 'UserHome', label: 'Hjem'},
            {name: 'MyProfile', label: 'Profil'},
            {name: 'MySettings', label: 'Innstillinger'},
            {name: 'LogOut', label: 'Logg ut'}
        ]
    },
    UserHomePageMain: {
        MainLinks: {
            Economy: {
                name: 'Economy',
                label: 'Økonomi',
                links: {
                    Accounting: { 
                        name: 'Accounting',
                        label: 'Regnskap',
                        info: '',
                        links: {
                            Account: {
                                name: 'Account', 
                                label: 'Kontoer',
                                info: '',
                                links: [
                                    {name: 'newAccount', label: 'Legg til ny konto', info: ''},
                                    {name: 'Account', label: 'Konto instillinger', info: ''}
                                ]
                            }, 
                            Transaction: {
                                name: 'Transaction', 
                                label: 'Transaksjoner',
                                info: '',
                                links: [
                                    {name: 'TransactionFilter', label: 'Filter'},
                                    {name: 'Account', label: 'Instillinger', info: ''},
                                    {name: 'TransactionAdd', label: 'Legg til'}
                                ]
                            }, 
                            Status: {
                                name: 'Status', 
                                label: 'Status',
                                info: '',
                                links: [
                                    {name: 'Account', label: 'Transaksjon instillinger', info: ''}
                                ]
                            }, 
                            FixedTransactions: {
                                name: 'FixedTransaction', 
                                label: 'Faste transaksjoner',
                                info: '',
                                links: [
                                    {name: 'Account', label: 'Instillinger', info: ''},
                                    {name: 'Account', label: 'Legg til'}
                                ]
                            }, 
                            // {name: 'Status', label: 'Status', },
                            // {name: 'FixedExpense', label: 'Faste transaksjoner'},
                            // {name: 'Suppliers', label: 'Leverandører'},
                        }
                    },
                    Loan: {
                        name: 'Loan',
                        label: 'Lån',
                        links: [
                        {name: 'AllLoan', label: 'Alle', info: 'Alle lån'},
                        {name: 'HousingLoan', label: 'Boliglån', info: 'Dine boliglån'},
                        {name: 'CarLoan', label: 'Billån', info: 'Dine billån'},
                        {name: 'ConsumerLoan', label: 'Forbrukslån', info: 'Dine forbrukslån' },
                        ]
                    },
                    Savings: {
                        name: 'Savings',
                        label: 'Sparing',
                        links: [

                        ]
                    },
                    Subscriptions: {
                        name: 'Subscriptions', 
                        label: 'Abonnementer',
                        links: [

                        ]
                    },
                },
            },
            Property: {
                name: 'Property', 
                label: 'Eiendom',
                links: {

                },
            },
            EducationAndCareer: {
                name: 'EducationAndCareer', 
                label: 'Utdanning/karriere',
                links: {

                },
            },
            FreePlan: {
                name: 'FreePlan', 
                label: 'Fritid',
                links: {

                },
            },
        },
        Links: [
            {name: 'Economy', label: 'Økonomi', childrenLinks: [
                {name: 'Accounting', label: 'Regnskap', info: '- Ditt regnskap', childrenLinks:[
                    {name: 'Account', label: 'Kontoer', },
                    {name: 'Transaction', label: 'Transaksjoner', },
                    {name: 'Status', label: 'Status', },
                    {name: 'FixedExpense', label: 'Faste utgifter'},
                    {name: 'Suppliers', label: 'Leverandører'},
                ]},
                {name: 'Loan', label: 'Lån', 
                //     childrenLinks:[
                //     {name: 'AllLoan', label: 'Alle', info: 'Alle lån'},
                //     {name: 'HousingLoan', label: 'Boliglån', info: 'Dine boliglån'},
                //     {name: 'CarLoan', label: 'Billån', info: 'Dine billån'},
                //     {name: 'ConsumerLoan', label: 'Forbrukslån', info: 'Dine forbrukslån' },
                // ]
                },
                {name: 'Savings', label: 'Sparing'},
                {name: 'Subscriptions', label: 'Abonnementer'}
            ]},
            {name: 'Property', label: 'Eiendom'},
            {name: 'EducationAndCareer', label: 'Utdanning og Karriere'},
            {name: 'FreePlan', label: 'Fritid/planlegger'},
        ]
    }

}


export {MyLinks, Links};