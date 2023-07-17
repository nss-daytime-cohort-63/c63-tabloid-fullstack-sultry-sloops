import { User } from "./User"
import React from "react"
import { useEffect,useState } from "react"
import { getDeactiveUsers } from "../modules/userManager";
import { useNavigate } from "react-router-dom";
export const DeactiveUserList =()=>{
    const [users,setUsers] = useState([]);
    const navigate = useNavigate()
    const getDeactive = ()=>{
     getDeactiveUsers().then(users => setUsers(users))   
    }

    useEffect(()=>{
        getDeactive();
    }, [])
    return <div>
         <h3>All Deactive Users</h3>
        <button onClick={()=> navigate("/users")}>Active Users</button>
        {
            users.map((user)=> ( <User user ={user} key ={user.id} />)
            
            )
        }
    </div>
}