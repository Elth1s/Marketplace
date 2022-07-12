import Tab from "@mui/material/Tab";

import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";

import { useState } from "react";

import Information from "./Information";
import Change from "./Change";

import { PaperStyled, TypographyStyled } from "../styled";

const tabs = ['Personal information', 'Change password and login']

const Profile = () => {
    const [valueTab, setValueTab] = useState<string>("0");

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <TabContext value={valueTab} >
            <TypographyStyled>Settings profile</TypographyStyled>
            <TabList onChange={handleChange}>
                {tabs.map((item, index) => (
                    <Tab
                        key={index}
                        label={item}
                        value={index.toString()}
                    />
                ))}
            </TabList >

            <TabPanel sx={{ p: "0px" }} value="0" >
                <PaperStyled>
                    <Information />
                </PaperStyled>
            </TabPanel>
            <TabPanel sx={{ p: "0px" }} value="1">
                <PaperStyled>
                    <Change />
                </PaperStyled>
            </TabPanel>
        </TabContext>
    )
}

export default Profile;