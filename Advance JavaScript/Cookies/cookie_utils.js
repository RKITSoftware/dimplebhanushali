export let cookies = {}

const updateCookies = () => {
    cookies = {}
    document.cookie.split(';').forEach((cookie) => {
        const [key, value] = cookie.split('=')

        if (!key)
            return
        cookies[key.trim()] = value.trim()
    })
}

export const setCookie = (key, value, date) => {
    if (key == undefined) {
        alert('Please pass value')
        return
    }

    if (date == undefined) {
        date = new Date()
        date.setDate(date.getDate() + 1)
    }

    const expiry = `expires = ${date.toUTCString()}`
    document.cookie = `${key} = ${value}; ${expiry}`
    updateCookies()
}

export const getCookie = (key) => {
    return cookies[key]
}

export const deleteAllCookies = () => {
    Object.keys(cookies).forEach((key) => {
        setCookie(key, '', new Date())
    })
}

updateCookies()
