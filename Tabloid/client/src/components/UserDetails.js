import React, { useEffect, useState } from "react";
import { getUserById, reactivateUser } from "../modules/userManager";
import { useNavigate, useParams } from "react-router-dom";
import { deactivateUser } from "../modules/userManager";
export const UserDetails = ({ userProfile }) => {
    const [user, setUser] = useState();
    const { id } = useParams();
    const navigate = useNavigate()
    useEffect(() => {
        getUserById(id).then(setUser)
    }, [])

    if (!user) {
        return null;
    }

    const deactivateCurrent = (event)=>{
        event.preventDefault()
        deactivateUser(user)
        .then(()=>{
            navigate(`/users/deactive`)
        })
    }

    const reactivateCurrent = (event)=>{
        event.preventDefault()
       reactivateUser(user)
        .then(()=>{
            navigate(`/users`)
        })
    }

    const date = new Date(user.createDateTime)
    return <div>
        <h3>User Details</h3>
        <section>{user.fullName}</section>

        {
            user.imageLocation === null ? <section><img height="200px" src="https://www.disneyclips.com/images/images/goofy_thinking.gif"></img></section> : <section><img height="200px" src={user.imageLocation}></img></section>
        }

        <section>Display Name: {user.displayName}</section>
        <section>Email: {user.email}</section>
        <section>Creation Date: {date.getMonth() + 1}/{date.getDate()}/{date.getFullYear()}</section>
        <section>User Type: {user.userType.name}</section>
        {
            user.isActive === true && userProfile?.userTypeId === 1 ? <button onClick={(ev)=>deactivateCurrent(ev)}>Deactivate</button> : ""
        }
        {
            user.isActive === false && userProfile?.userTypeId === 1 ? <button onClick={(ev)=>reactivateCurrent(ev)}>Reactivate</button> : ""
        }

    </div>
}