import React from "react";
import { useState, useEffect } from "react";
import { getAllTags } from "../modules/tagManager";
import Tag from "./Tag";
import TagMaker from "./TagMaker";

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
        <TagMaker getTags={getTags} />
        <div>Tag List</div>
        <ul>
            {tags.map((t) => {
                return <Tag t={t} key={t.id} />
            })}
        </ul>
    </>
}
export default TagList;