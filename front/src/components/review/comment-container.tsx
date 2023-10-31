import React from 'react';

// Comment component
interface CommentProps {
  username: string;
  body: string;
}


function Comment({ username, body }: CommentProps) {
  return (
    <div className="border border-gray-400 rounded-md p-3 mb-1">
      <div className="flex flex-wrap justify-between">
        <div className="user-name">{username}</div>
        <button className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded-full">
         Pranešti 
        </button>
      </div>
      <div className="comment-body">{body}</div>
    </div>
  );
}

// CommentContainer component that holds multiple comments
interface CommentContainerProps {
  comments: CommentProps[];
}

function CommentContainer({ comments }: CommentContainerProps) {
  return (
    <div className="comment-section">
      {comments.map((comment, index) => (
        <Comment key={index} username={comment.username} body={comment.body} />
      ))}
    </div>
  );
}

export default CommentContainer;
