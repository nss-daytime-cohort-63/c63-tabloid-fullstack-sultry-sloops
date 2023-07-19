import React from "react";
import { useState, useEffect } from "react";
import { getAllCategories } from "../modules/categoryManager";
import Category from "./Category";
import { CategoryAdd } from "./CategoryAdd";
import { Modal, ModalBody, ModalHeader, ModalFooter } from "reactstrap";
import { deleteCategory } from "../modules/categoryManager";


const CategoryList = () => {
    const [categories, setCategories] = useState([]);
    const [show, setShow] = useState(false);
    const [selectCategory, setSelectCategory] = useState({
        id: 0,
        name: ""
    });


    const finalDelete = (c) => {
        deleteCategory(c.id)
            .then(getCategories())

    }


    const getCategories = () => {
        getAllCategories().then(data => setCategories(data));

    }
    useEffect(
        () => {
            getCategories();
        }, []
    );

    useEffect(
        () => {
            if (selectCategory.id != 0) {
                finalDelete(selectCategory)
            }
        }, [selectCategory]
    );

    return <>
        <CategoryAdd getCategories={getCategories} />
        <div>Category List</div>
        <ul>
            {categories.map((c) => {
                return <Category c={c} key={c.id} setSelectCategory={setSelectCategory} />
            })}
        </ul>


    </>
}

export default CategoryList;
