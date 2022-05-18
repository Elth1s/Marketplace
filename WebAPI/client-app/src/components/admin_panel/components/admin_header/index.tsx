import React from "react";
import "./index.css";
import { HiOutlineMenuAlt1, HiOutlineSearch } from "react-icons/hi";
import { VscBellDot } from "react-icons/vsc";
import AccountPhoto from "../../img/will.png";
import {GoTriangleDown}from "react-icons/go"

const AdminHeader = (props:any) => {

  return (
    <div className="header_container">
      <HiOutlineMenuAlt1 onClick={props.openMenu} size={30} />
      <div className="profile_options">
        <HiOutlineSearch size={30} style={{ marginRight: "10px" }} />
        <VscBellDot size={30} style={{ marginRight: "10px" }} />
        <hr className="line_between_icons_and_profile" />
        <div className="account">
          <img src={AccountPhoto} alt="" />
          <span>User_Name</span>
          <GoTriangleDown size={15} style={{ marginLeft: "10px" }} />
        </div>
      </div>
    </div>
  );
};

export default AdminHeader;
