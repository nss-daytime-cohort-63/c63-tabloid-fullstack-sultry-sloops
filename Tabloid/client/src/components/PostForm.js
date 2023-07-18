import { useNavigate } from "react-router-dom"
import { Form, FormGroup, Input, Label } from "reactstrap";
import { getAllCategories } from "../modules/categoryManager";
import { useState } from "react";
import { addNewPost } from "../modules/postManager";


export const PostForm = ({ getPosts, userProfile, getAllCategories }) => {
    const navigate = useNavigate();
    const [newPost, updatePost] = useState({
        Title: '',
        Content: '',
        ImageLocation: '',
        CreateDateTime: '', // need this to be the current date/time
        isApproved: 1, // need this to automatically assign 1
        CategoryId: '',
        UserProfileId: ''
    });

    const newPostButton = (event) => {
        event.preventDefault();
        let nP = {
            Title: newPost.Title,
            Content: newPost.Content,
            ImageLocation: newPost.ImageLocation,
            CreateDateTime: newPost.CreateDateTime,
            isApproved: newPost.isApproved,
            CategoryId: newPost.CategoryId,
            UserProfileId: newPost.UserProfileId
        };
        addNewPost(nP).then((post) => {
            getPosts();
        })
    };

    const categories = getAllCategories();

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
                                copy.CategoryId = event.target.value
                                updatePost(copy);
                            }
                        }>
                        <option value="0">Select category</option>
                        {
                            categories.map(
                                (category) => {
                                    return <option key={category.id}>
                                        {category.Name}
                                    </option>
                                }
                            )
                        }
                    </select>

                </div>

            </div>

            {/* <Form>
            <FormGroup>
                <Label for="title">Title</Label>
                <Input type="text" name="title" id="title" placeholder="Post Title" value={post.Title}
                    onChange={handleInputChange} />
            </FormGroup>
            <FormGroup>
                <Label for="Content">Content</Label>
                <Input type="textarea" name="content" id="content" placeholder="Post Content" value={post.Content}
                    onChange={handleInputChange} />
            </FormGroup>
            <FormGroup>
                <Label for="ImageLocation">Image Location URL</Label>
                <Input type="text" name="imageLocation" id="imageLocation" placeholder="Image Location URL" value={post.ImageLocation}
                    onChange={handleInputChange} />
            </FormGroup>

            //start here

        </Form> */}
        </>
    )
}