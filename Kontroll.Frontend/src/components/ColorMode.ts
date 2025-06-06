import { ref} from 'vue';

const colorMode = ref(localStorage.getItem('colorMode') || 'dark');
document.documentElement.setAttribute('data-bs-theme', colorMode.value);
const toggleColorMode = () => {
    colorMode.value = colorMode.value === 'light' ? 'dark' : colorMode.value === 'dark' ? 'dark-cookie' : colorMode.value === 'dark-cookie' ? 'blue' : colorMode.value === 'blue' ? 'frosted' : 'light';
    localStorage.setItem('colorMode', colorMode.value);
    document.documentElement.setAttribute('data-bs-theme', colorMode.value);
};




export {colorMode, toggleColorMode};
