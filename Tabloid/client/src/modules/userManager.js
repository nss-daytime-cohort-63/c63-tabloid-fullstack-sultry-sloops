
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

export const getDeactiveUsers = ()=>{
    return fetch(`${baseUrl}/deactive`)
    .then((res)=>res.json())
}

export const getAllAdmins = () =>{
    return fetch(`${baseUrl}/admins`)
    .then((res)=>res.json())
}

export const deactivateUser =(user)=>{
    return fetch(`${baseUrl}/deactivate/${user.id}`,{
        method : "PUT",
        headers: {
            "Content-Type" : "application/json",
        },
        body: JSON.stringify(user)
    })
    
}

export const reactivateUser =(user)=>{
    return fetch(`${baseUrl}/reactivate/${user.id}`,{
        method : "PUT",
        headers: {
            "Content-Type" : "application/json",
        },
        body: JSON.stringify(user)
    })
    
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