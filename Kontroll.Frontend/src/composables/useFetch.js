export const fetchData = async (url, method = "GET", body = null, loading, error) => {
  loading.value = true;
  error.value = null;

  try {
    const options = { method };

    if (body && !(body instanceof FormData)) {
      options.headers = {
        'Content-Type': 'application/json'
      };
      options.body = JSON.stringify(body);
    } else {
      // Don't set Content-Type for FormData
      options.body = body;
    }

    const result = await fetch(url, options);
    const text = await result.text();
    return text ? JSON.parse(text) : null;
  } catch (err) {
    error.value = err.message;
    return null;
  } finally {
    loading.value = false;
  }
};
