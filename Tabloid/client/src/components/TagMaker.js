import React from "react";
import { useState } from "react";
import { addTag } from "../modules/tagManager";
import { useNavigate } from "react-router-dom";

const TagMaker = ({ getTags }) => {
    const [newTag, updateTag] = useState({
        Name: ""
    });
    const navigate = useNavigate();

    const newTagButton = (event) => {
        event.preventDefault();
        let nT = {
            Name: newTag.Name
        };
        addTag(nT).then((t) => {
            getTags();
        })
    }

    return (
        <div>
            <center><h4>Add New Tag</h4></center>
            <div style={{ margin: "1rem" }}>
                <div>Tag Name</div>
                <input
                    required autoFocus
                    type="text"
                    placeholder="Enter Tag Name"
                    value={newTag.Name}
                    onChange={
                        (event) => {
                            const copy = { ...newTag }
                            copy.Name = event.target.value
                            updateTag(copy);
                        }
                    } />
            </div>
            <button onClick={event => newTagButton(event)}>Add Tag</button>
        </div>
    );
};
export default TagMaker;