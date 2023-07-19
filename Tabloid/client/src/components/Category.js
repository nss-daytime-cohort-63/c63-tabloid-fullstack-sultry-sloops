import React from "react";
import { Button, Input } from "reactstrap";
import { deleteCategory } from "../modules/categoryManager";


const Category = ({ c, setSelectCategory }) => {

    return (
        <>
            <li>{c.name}</li>
            <Button onClick={() => {
                setSelectCategory(c)
            }}>Delete</Button>

        </>
    );
}


export default Category;