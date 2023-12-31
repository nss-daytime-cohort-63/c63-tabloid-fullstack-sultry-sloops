import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import { UserList } from "./UserList";
import { UserDetails } from "./UserDetails";
import TagList from "./TagList";
import { PostList } from "./PostList";
import { UserEdit } from "./UserEdit";
import CategoryList from "./CategoryList";
import PostDetails from "./PostDetails";
import { DeactiveUserList } from "./UserDList";
import { MyPostList } from "./MyPostList";
import { PostForm } from "./PostForm";

export default function ApplicationViews({ isLoggedIn, userProfile }) {
  return (
    <main>
      <Routes>
        <Route path="/">
          <Route
            index
            element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
          />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path ="users" element={<UserList userProfile={userProfile}/>} />
          <Route path ="users/deactive" element={<DeactiveUserList userProfile={userProfile}/>} />
          <Route path ="users/:id" element={<UserDetails userProfile={userProfile}/>} />
          <Route path ="users/edit/:id" element={<UserEdit />} />
          <Route path="tag" element={<TagList />} />
          <Route path="*" element={<p>Whoops, nothing here...</p>} />
          <Route path="posts" element={<PostList />} />
          <Route path="addPost" element={<PostForm userProfile={userProfile} />} />
          <Route path="myposts" element={<MyPostList userProfile={userProfile} />} />
          <Route
            path="posts"
            element={<PostList userProfile={userProfile} />}
          />
          <Route path="category" element={<CategoryList />} />
          <Route
            path="posts/:id"
            element={<PostDetails userProfile={userProfile} />}
          />
        </Route>
      </Routes>
    </main>
  );
}
