import { Typography, Tab, Box } from "@mui/material";
import { TabContext, TabList, TabPanel } from "@mui/lab";

import { useState } from "react";

import Information from "./Information";
import Change from "./Change";

import { useTranslation } from "react-i18next";

const Profile = () => {
    const { t } = useTranslation();

    const tabs = [
        { label: `${t("pages.user.personalInformation.tabs.personalInfo")}` },
        { label: `${t("pages.user.personalInformation.tabs.changePasswordAndLogin")}` }
    ]

    const [valueTab, setValueTab] = useState<string>("0");

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <>
            <Typography variant="h1" sx={{ mb: "30px" }}>{t("pages.user.personalInformation.title")}</Typography>
            <TabContext value={valueTab} >
                <TabList onChange={handleChange} >
                    {tabs.map((item, index) => (
                        <Tab
                            key={index}
                            label={item.label}
                            value={index.toString()}
                            sx={{
                                fontSize: "27px",
                                padding: "0px",
                                minWidth: "auto",
                                textTransform: "none",
                                color: "inherit",
                                "&:nth-of-type(1)": {
                                    mr: "auto",
                                },
                                "&& .MuiTouchRipple-child": {
                                    backgroundColor: "transparent"
                                }
                            }}
                        />
                    ))}
                </TabList >

                <Box sx={{ border: "1px solid #7e7e7e", borderRadius: "10px", p: "30px", mt: "25px" }}>
                    <TabPanel sx={{ p: "0px" }} value="0" >
                        <Information />
                    </TabPanel>
                    <TabPanel sx={{ p: "0px" }} value="1">
                        <Change />
                    </TabPanel>
                </Box>
            </TabContext>
        </>
    )
}

export default Profile;