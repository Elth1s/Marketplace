import React,{useState} from "react";
import "./index.css";
import {GoPrimitiveDot}from "react-icons/go"

const MenuItem = (props:any) =>{


return(
<>

<div id={props.id} className={props.isActive ? "menu_item item_active" : "menu_item"}>      
         <GoPrimitiveDot size={25} style={{marginRight:"10px"}} fill={props.isActive ? "#fff":"#FDA906"}/>
         <span>{props.text}</span>
    </div>
</>
    
)
}

export default MenuItem;