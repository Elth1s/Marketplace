import React, { useState } from "react";
import "./index.css";
import Logo from "../../../../logo.svg";
import MenuItem from "./menu_item";
const arr = [
  { id: 1, text: "Name_1", isActive: false },
  { id: 2, text: "Name_2", isActive: false },
  { id: 3, text: "Name_3", isActive: false },
];

const LeftMenu = (props:any) => {
  const [options, setOptions] = useState(arr);

  const selectItem = (id: any) => {
    let arr = options.map((i) => {
      if (i.id == id) {
        i.isActive = !i.isActive;
      } else {
        i.isActive = false;
      }
      return i;
    });
    setOptions(arr);
  };

  return (
    <div className="left_menu_container">
      <div className="logo_container">
        <img src={Logo} alt="" />
      </div>
      <div className="custom_menu_items">
        {options.map((i: any) => {
          return (
            <div id={i.id} onClick={() => selectItem(i.id)}>
              <MenuItem text={i.text} isActive={i.isActive} />
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default LeftMenu;
