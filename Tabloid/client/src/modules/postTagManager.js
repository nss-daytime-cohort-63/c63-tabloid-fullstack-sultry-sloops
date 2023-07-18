const baseUrl = "/api/PostTag";

export const getAllPostTags = () => {
  return fetch(baseUrl).then((response) => response.json());
};

export const getPostTagsByPostId = (id) => {
  return fetch(`${baseUrl}/${id}`).then((response) => response.json());
};

export const addPostTag = (postTag) => {
  return fetch(`${baseUrl}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(postTag),
  });
};

export const deletePostTag = (id) => {
  return fetch(`${baseUrl}/${id}`, {
    method: "DELETE",
  });
};
