import EconomyView from "@/views/EconomyView.vue";

const MyLinks = {
    HomePageLinks: [
        {name: 'HomeSearch', label: 'Søk', svg: `<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="m15.75 15.75-2.489-2.489m0 0a3.375 3.375 0 1 0-4.773-4.773 3.375 3.375 0 0 0 4.774 4.774ZM21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" /></svg>`},
        {name: 'About', label: 'Om oss', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z" /></svg>'},
        {name: 'UserHome', label: 'Login', svg: '<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6"><path stroke-linecap="round" stroke-linejoin="round" d="M8.25 9V5.25A2.25 2.25 0 0 1 10.5 3h6a2.25 2.25 0 0 1 2.25 2.25v13.5A2.25 2.25 0 0 1 16.5 21h-6a2.25 2.25 0 0 1-2.25-2.25V15M12 9l3 3m0 0-3 3m3-3H2.25" /></svg>'},
    ],
    Main: [
        {name: 'Economy', label: 'Økonomi'},
        {name: 'Property', label: 'Eiendom'},
    ],
    Economy: [
        { name: 'Transaction', label: 'Transaksjoner' },
        { name: 'FixedExpense', label: 'Faste Utgifter' },
        { name: 'Suppliers', label: 'Leverandører'},
    ],

};

const Lnker = {
    HomePage: {
        Links: [
            {name: 'About', label: 'Om oss'},
        ]
    }
}


export {MyLinks};