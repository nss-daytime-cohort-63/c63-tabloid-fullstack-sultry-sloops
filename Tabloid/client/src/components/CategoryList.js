import React from "react";
import { useState, useEffect } from "react";
import { getAllCategories } from "../modules/categoryManager";
import Category from "./Category";

const CategoryList = () => {
    const [categories, setCategories] = useState([]);


    const getCategories = () => {
        getAllCategories().then(data => setCategories(data));

    }
    useEffect(
        () => {
            getCategories();
        }, []
    );

    return <>
        <div>Category List</div>
        <ul>
            {categories.map((c) => {
                return <Category c={c} key={c.id} />
            })}
        </ul>


    </>
}

export default CategoryList;
