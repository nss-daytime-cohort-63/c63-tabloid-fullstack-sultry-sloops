import React from "react";
import { useState, useEffect } from "react";
import { getAllCategories } from "../modules/categoryManager";
import Category from "./Category";
import { CategoryAdd } from "./CategoryAdd";
import { Modal, ModalBody, ModalHeader, ModalFooter } from "reactstrap";


const CategoryList = () => {
    const [categories, setCategories] = useState([]);
    const [show, setShow] = useState(false);
    const [selectCategory, setSelectCategory] = useState({ id: 0, name: "" });




    const getCategories = () => {
        getAllCategories().then(data => setCategories(data));

    }
    useEffect(
        () => {
            getCategories();
        }, []
    );

    return <>
        <CategoryAdd getCategories={getCategories} />
        <div>Category List</div>
        <ul>
            {categories.map((c) => {
                return <Category c={c} key={c.id} />
            })}
        </ul>


    </>
}

export default CategoryList;
