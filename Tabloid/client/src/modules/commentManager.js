const baseUrl = "/api/comment"

//adds a new comment to the database
export const addComment = (comment) => {
    return fetch(`${baseUrl}/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(comment)
    });
};

//updates a comment that is already in the database
export const updateComment = (comment) => {
    return fetch(`${baseUrl}/update/${comment.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(comment)
    });
};

//deletes a comment from the database
export const deleteComment = (id) => {
    return fetch(`${baseUrl}/delete/${id}`, {
        method: "DELETE"
    });
};