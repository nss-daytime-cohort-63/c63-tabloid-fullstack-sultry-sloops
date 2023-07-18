import React, { useEffect, useState } from "react";
import { getAllTypes, getUserById, updateUser } from "../modules/userManager";
import { useNavigate, useParams } from "react-router-dom";
import { getAllAdmins } from "../modules/userManager";
export const UserEdit =()=>{
    const [user,setUser] = useState();
    const [original, setOriginal] = useState();
    const [types, setTypes] = useState();
    const [admins,setAdmins] = useState();
    const { id } = useParams();
    const navigate = useNavigate();
    useEffect(()=>{
        getAllAdmins().then(users=>setAdmins(users))
    },[])
    useEffect(()=>{
        getUserById(id).then(setUser)
    },[])
    useEffect(()=>{
        getUserById(id).then(setOriginal)
    },[])

    useEffect(()=>{
        getAllTypes().then(types => setTypes(types))
    },[])

    if(!user){
        return null;
    }
    if(!original){
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
    const deactivateError =()=>{
        const myDiv=  document.querySelector("#deactivate-error")
        myDiv.innerHTML = "You cant change the type of the last Admin"
      }
    const adminsLength = admins.length
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
            {
                original.userTypeId === 1 ? 
                adminsLength > 1  ?
                <button onClick={(ev)=> editUserType(ev)}>Save</button> 
                : 
                user.userTypeId === 1 ? 
                <button onClick={(ev)=> editUserType(ev)}>Save</button> :
                <button onClick={()=>deactivateError()}>Save</button>
                :
                <button onClick={(ev)=> editUserType(ev)}>Save</button>
            }
            
            <button onClick={()=>navigate("/users")}>Cancel</button>
            <div id="deactivate-error">

        </div>
    </div>
}