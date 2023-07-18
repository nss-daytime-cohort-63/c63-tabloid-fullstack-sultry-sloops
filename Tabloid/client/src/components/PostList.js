import { Post } from "./Post";
import { getAllPastApprovedPosts, getAllPosts } from "../modules/postManager";
import { useState, useEffect } from "react";
import { Button, Table } from "reactstrap";

export const PostList = ({ userProfile }) => {
    const [posts, setPosts] = useState([]);

    const getPosts = () => {
        getAllPastApprovedPosts().then((data) => setPosts(data));
    };

    useEffect(() => {
        getPosts();
    }, []);

    return (
        <>
            <Button href="/addPost">Add New Post</Button>
            <Table hover responsive size="">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                        <th>Category</th>
                    </tr>
                </thead>
                <tbody>
                    {posts.map((p) => {
                        return <Post post={p} key={p.id} userProfile={userProfile} />;
                    })}
                </tbody>
            </Table>
        </>
    );
};
