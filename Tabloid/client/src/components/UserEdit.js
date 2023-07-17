import React, { useEffect, useState } from "react";
import { getAllTypes, getUserById, updateUser } from "../modules/userManager";
import { useNavigate, useParams } from "react-router-dom";
export const UserEdit =()=>{
    const [user,setUser] = useState();
    const [types, setTypes] = useState([]);
    const { id } = useParams();
    const navigate = useNavigate();
    useEffect(()=>{
        getUserById(id).then(setUser)
    },[])

    useEffect(()=>{
        getAllTypes().then(types => setTypes(types))
    },[])

    if(!user){
        return null;
    }
const myId = parseInt(id)
    const editUserType = (event) =>{
        event.preventDefault()
        updateUser(myId,user)
        .then(()=>{
            navigate("/users")
        })
    }
    return <div>
        <h3>Change {user.fullName}'s User Type</h3>
        <section><select  value ={user.userTypeId} onChange={
            (evt) =>{
                const copy ={...user}
                copy.userTypeId = parseInt(evt.target.value)
                setUser(copy)
            }
        }>
            
            {
                types.map(type=>{
                    return <option key={type.id} value ={type.id}>{type.name}</option>
                })
            }
            </select></section>
            <button onClick={(ev)=> editUserType(ev)}>Save</button>
            <button onClick={()=>navigate("/users")}>Cancel</button>
    </div>
}