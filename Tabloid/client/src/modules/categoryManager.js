const baseUrl = "/api/category"

//gets all categories with id and name
export const getAllCategories = () => {
    return fetch(`${baseUrl}`)
        .then((res) => res.json())
}

//adds a new category to the database
export const addCategory = (category) => {
    return fetch(`${baseUrl}/add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(category)
    })
}

//function to delete a category
export const deleteCategory = (id) => {
    return fetch(`${baseUrl}/delete/${id}`, {
        method: "DELETE"
    })
}