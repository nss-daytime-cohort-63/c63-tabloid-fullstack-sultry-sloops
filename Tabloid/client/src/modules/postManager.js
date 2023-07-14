const baseUrl = "/api/Post/"

export const getAllVideos = () => {
    return fetch(baseUrl)
        .then(response => response.json())

}