const baseUrl = '/api/userprofile';
const typeUrl = `/api/usertype`;
export const getAllUsers = () =>{
    return fetch(baseUrl)
    .then((res)=> res.json())
}

export const getAllTypes = () =>{
    return fetch(typeUrl)
    .then((res)=> res.json())
}
export const getUserById =(id) =>{
    return fetch(`${baseUrl}/${id}`).then((res)=>res.json())
}

export const updateUser =(id, user) =>{
    return fetch(`${baseUrl}/${id}`,{
        method : "PUT",
        headers: {
            "Content-Type" : "application/json",
        },
        body: JSON.stringify(user)
    });
};