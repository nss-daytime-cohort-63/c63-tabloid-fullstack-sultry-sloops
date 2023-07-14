import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import { UserList } from "./UserList";
import { UserDetails } from "./UserDetails";
import TagList from "./TagList";
import { PostList } from "./PostList";
import CategoryList from "./CategoryList";

export default function ApplicationViews({ isLoggedIn }) {
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
          <Route path="users" element={<UserList />} />
          <Route path="users/:id" element={<UserDetails />} />
          <Route path="tag" element={<TagList />} />
          <Route path="*" element={<p>Whoops, nothing here...</p>} />
          <Route path="posts" element={<PostList />} />
          <Route path="category" element={<CategoryList />} />
        </Route>
      </Routes>
    </main>
  );
}
