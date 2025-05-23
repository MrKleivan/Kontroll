const fetchData = async (url, method = "GET", body = null, loading, error) => {
    loading.value = true;
    error.value = null;

    try {
        const options = { method };
        const result = await fetch(url, options);

        const text = await result.text();
        console.log(text);
        return text ? JSON.parse(text) : [];
    } catch (err) {
        error.value = err.message;
        return [];
    } finally {
        loading.value = false;
    }
    
};

export {fetchData};