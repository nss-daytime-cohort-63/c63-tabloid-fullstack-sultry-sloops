const baseUrl = "/api/Post";

export const getAllPosts = () => {
    return fetch(baseUrl).then((response) => response.json());
};

export const getAllPastApprovedPosts = () => {
    return fetch(`${baseUrl}/GetApprovedPastPosts`).then((response) =>
        response.json()
    );
};

export const getPostById = (id) => {
    return fetch(`${baseUrl}/${id}`).then((response) =>
        response.json()
    );
};

export const getPostByUserId = (userId) => {
    return fetch(`${baseUrl}/GetByUserId/${userId}`).then((response) =>
        response.json()
    );
};