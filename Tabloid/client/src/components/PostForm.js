import { useNavigate } from "react-router-dom"
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { getAllCategories } from "../modules/categoryManager";
import { useState, useEffect } from "react";
import { addNewPost } from "../modules/postManager";


export const PostForm = ({ getPosts, userProfile }) => {
    const navigate = useNavigate();

    const [categories, setCategories] = useState([]);

    // to offset timezone to local time
    var tzoffset = (new Date()).getTimezoneOffset() * 60000; //offset in milliseconds
    var localISOTime = (new Date(Date.now() - tzoffset)).toISOString().slice(0, -1);

    const [newPost, updatePost] = useState({
        Title: '',
        Content: '',
        ImageLocation: '',
        CreateDateTime: localISOTime,
        PublishDateTime: localISOTime,
        isApproved: true,
        CategoryId: 0,
        UserProfileId: 0
    });

    useEffect(
        () => {
            if (userProfile) {
                const copy = { ...newPost }
                copy.UserProfileId = userProfile.id
                updatePost(copy)
            }
        },
        [userProfile]
    )

    const newPostButton = (event) => {
        event.preventDefault();
        let nP = {
            Title: newPost.Title,
            Content: newPost.Content,
            ImageLocation: newPost.ImageLocation,
            CreateDateTime: newPost.CreateDateTime,
            PublishDateTime: newPost.PublishDateTime,
            isApproved: newPost.isApproved,
            CategoryId: newPost.CategoryId,
            UserProfileId: newPost.UserProfileId
        };
        addNewPost(nP).then((post) => {
            navigate('/myPosts');
        })
    };

    const getCategories = () => {
        getAllCategories().then(data => setCategories(data));

    }

    useEffect(
        () => {
            getCategories()
        },
        []
    )



    return (
        <>
            <div>
                <center><h4>Add New Post</h4></center>
                <div style={{ margin: "1rem" }}>

                    <div>Post Title</div>
                    <input
                        required autoFocus
                        type="text"
                        placeholder="Enter Post Title"
                        value={newPost.Title}
                        onChange={
                            (event) => {
                                const copy = { ...newPost }
                                copy.Title = event.target.value
                                updatePost(copy);
                            }
                        }
                    />

                    <div>Post Content</div>
                    <input
                        required autoFocus
                        type="textarea"
                        placeholder="Enter Post Content"
                        value={newPost.Content}
                        onChange={
                            (event) => {
                                const copy = { ...newPost }
                                copy.Content = event.target.value
                                updatePost(copy);
                            }
                        }
                    />

                    <div>Post Image URL</div>
                    <input
                        required autoFocus
                        type="text"
                        placeholder="Enter Image URL"
                        value={newPost.ImageLocation}
                        onChange={
                            (event) => {
                                const copy = { ...newPost }
                                copy.ImageLocation = event.target.value
                                updatePost(copy);
                            }
                        }
                    />

                    <div>Category</div>
                    <select id="CategoryId"
                        required
                        value={newPost.CategoryId}
                        onChange={
                            (event) => {
                                const copy = { ...newPost }
                                copy.CategoryId = parseInt(event.target.value)
                                updatePost(copy);
                            }
                        }>
                        <option value="0">Select category</option>
                        {
                            categories.length > 0 ?
                                categories.map(
                                    (category) => {
                                        return <option key={category.id} value={category.id}>
                                            {category.name}
                                        </option>
                                    }
                                ) : ""
                        }
                    </select>
                    <div>
                        <Button onClick={newPostButton}>Add Post</Button>
                    </div>
                </div>

            </div>
        </>
    )
}