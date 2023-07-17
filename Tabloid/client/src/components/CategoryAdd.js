import React from "react";
import { useState } from "react";
import { addCategory } from "../modules/categoryManager";
import { useNavigate } from "react-router-dom";

export const CategoryAdd = ({ getCategories }) => {

    const [newCategory, updateCategory] = useState({
        name: ""
    });

    const newCategoryButton = (event) => {
        event.preventDefault();
        let nC = {
            name: newCategory.name
        }
        addCategory(nC).then((c) => {
            getCategories()
        })
    }

    return (
        <div>
            <center><h4>Add New Category</h4></center>
            <div style={{ margin: "1rem" }}>
                <div>Category Name</div>
                <input
                    required autoFocus
                    type="text"
                    placeholder="Enter Category Name"
                    value={newCategory.name}
                    onChange={
                        (event) => {
                            const copy = { ...newCategory }
                            copy.name = event.target.value
                            updateCategory(copy);
                        }
                    } />
            </div>
            <button onClick={event => newCategoryButton(event)}>Add Category</button>
        </div>
    )
}