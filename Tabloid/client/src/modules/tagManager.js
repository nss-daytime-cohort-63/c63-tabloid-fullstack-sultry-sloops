const baseUrl = "/api/tag"

//gets the full list of tags from the database with .id and .name
export const getAllTags = () => {
    return fetch(`${baseUrl}`)
        .then(res => res.json())
};

//adds a tag to the database
export const addTag = (tag) => {
    return fetch(`${baseUrl}/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(tag)
    });
};

//deletes a tag from the database
export const deleteTag = (id) => {
    return fetch(`${baseUrl}/delete/${id}`, {
        method: "DELETE"
    });
};