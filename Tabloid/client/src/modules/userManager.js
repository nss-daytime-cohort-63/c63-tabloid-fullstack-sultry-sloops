const baseUrl = '/api/userprofile';

export const getAllUsers = () =>{
    return fetch(baseUrl)
    .then((res)=> res.json())
}

export const getUserById =(id) =>{
    return fetch(`${baseUrl}/${id}`).then((res)=>res.json())
}