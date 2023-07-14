import React from "react";
import { useState, useEffect } from "react";
import { getAllTags } from "../modules/tagManager";
import Tag from "./Tag";

const TagList = () => {
    const [tags, setTags] = useState([]);

    const getTags = () => {
        getAllTags().then(data => setTags(data));
    }
    useEffect(
        () => {
            getTags();
        }, []
    );

    return <>
        <div>Tag List</div>
        <ul>
            {tags.map((t) => {
                return <Tag t={t} key={t.id} />
            })}
        </ul>
    </>
}
export default TagList;