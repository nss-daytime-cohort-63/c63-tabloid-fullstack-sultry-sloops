import { useNavigate } from "react-router-dom"
import { deactivateUser, getAllUsers } from "../modules/userManager"
import { User } from "./User"
import React from "react"
import { useEffect,useState } from "react"
export const UserList =() =>{
    const [users,setUsers] = useState([]);
    const navigate = useNavigate();
    const getUsers =()=>{
        getAllUsers().then(users => setUsers(users))
    }

    useEffect(()=>{
        getUsers();
    }, []);

    
    return <div>
        <h3>All Users</h3>
        <button onClick ={()=>navigate(`deactive`)}>Deactive Users</button>
        {
            users.map((user)=> ( <User user ={user} key ={user.id} />)
            
            )
        }

    </div>
}