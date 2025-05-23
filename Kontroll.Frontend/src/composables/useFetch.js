export const fetchData = async (url, method = "GET", body = null, loading, error) => {
  loading.value = true
  error.value = null

  try {
    const options = { method, headers: { 'Content-Type': 'application/json' } }
    if (body) options.body = JSON.stringify(body)

    const result = await fetch(url, options)
    const text = await result.text()
    return text ? JSON.parse(text) : null
  } catch (err) {
    error.value = err.message
    return null
  } finally {
    loading.value = false
  }
}
