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

export const getPostsByUserId = (userId) => {
    return fetch(`${baseUrl}/GetByUserId/${userId}`).then((response) =>
        response.json()
    );
};

export const addNewPost = (post) => {
    // return getToken().then((token) => {
    return fetch(`baseUrl/add`, {
        method: "POST",
        headers: {
            // Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify(post),
    }).then((resp) => {
        if (resp.ok) {
            return resp.json();
        } else if (resp.status === 401) {
            throw new Error("Unauthorized");
        } else {
            throw new Error(
                "An unknown error occurred while trying to save a new post.",
            );
        }
    });
    // });
};