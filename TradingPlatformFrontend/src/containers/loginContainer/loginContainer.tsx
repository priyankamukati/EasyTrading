import { Amplify, Auth } from "aws-amplify";

import Alert from "react-bootstrap/Alert";
import {
  Authenticator,
  useAuthenticator,
  withAuthenticator,
  WithAuthenticatorProps,
} from "@aws-amplify/ui-react";
import "@aws-amplify/ui-react/styles.css";
import { useNavigate } from "react-router-dom";

import config from "../../aws-exports-new";
import { Dispatch, FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { GetUserInfo, SaveUserInfo, UserInfo } from "../../model/userInfo";
import { saveUserInfo, saveUserInfoEmpty } from "../../store/saveUserInfo.slice";
import { State } from "../../model/state";
import { LoadingState } from "../../model/loadingState";
import { getUserInfo, getUserInfoEmpty } from "../../store/getUserInfo.slice";
Amplify.configure(config);

interface ILoginContainerProps extends WithAuthenticatorProps {
  saveUserInfo: typeof saveUserInfo;
  saveUserInfoResponse: State<SaveUserInfo>;

  getUserInfo: typeof getUserInfo;
  getUserInfoResponse: State<GetUserInfo>;

  getUserInfoEmpty: typeof getUserInfoEmpty;
  saveUserInfoEmpty: typeof saveUserInfoEmpty;
}

const LoginContainer: FunctionComponent<ILoginContainerProps> & {
  defaultProps: Partial<ILoginContainerProps>;
} = ({
  signOut,
  user,
  saveUserInfo,
  saveUserInfoResponse,
  getUserInfo,
  getUserInfoResponse,
  getUserInfoEmpty,
  saveUserInfoEmpty
}: ILoginContainerProps) => {
  const navigate = useNavigate();

  const services = {
    async handleSignUp(formData: any) {
      let { username, password, attributes } = formData;
      // custom username
      username = username.toLowerCase();
      attributes.email = attributes.email.toLowerCase();

      return Auth.signUp({
        username,
        password,
        attributes,
        autoSignIn: {
          enabled: true,
        },
      });
    },
  };
  

  const { route } = useAuthenticator(context => [context.route]);


  useEffect(() => {
    if (user && route === 'authenticated') {
      getUserInfo();
  }}, [user, getUserInfo]);


  useEffect(() => {
    if (user && getUserInfoResponse.error) {
      console.log("error: ", getUserInfoResponse.error)

      // the user is not present
      const userInfo = new UserInfo();
      userInfo.full_name = user.attributes?.name;
      userInfo.username = user.attributes?.preferred_username;
      userInfo.passcode = user.attributes?.nickname;
      userInfo.email = user.attributes?.email; 
      saveUserInfo(userInfo);
    } else {

      if (getUserInfoResponse.data && getUserInfoResponse.data.type) {
        console.log("this is the type : ", getUserInfoResponse.data.type)
        if (getUserInfoResponse.data.type === "admin") {
          console.log("get i am admin : ", getUserInfoResponse.data.type)
          navigate('/admin')
      } else {
         console.log("get i am trader : ", getUserInfoResponse.data.type)
         navigate('/home')
      }
    }
  }
  }, [user, navigate, getUserInfoResponse]);



  useEffect(() => {
    if (saveUserInfoResponse.loading === LoadingState.Idle && saveUserInfoResponse.data?.type !== undefined) {

      console.log(" saveUserInfoResponse: ", saveUserInfoResponse.loading, saveUserInfoResponse.data?.type)
      console.log("this is the type : ", saveUserInfoResponse.data.type)


      if (saveUserInfoResponse.data.type === "admin") {
          console.log("save i am admin : ", saveUserInfoResponse.data.type)
          navigate('/admin')
      } else {
         console.log("save i am trader : ", saveUserInfoResponse.data.type)
         navigate('/home')
      }

  }}, [saveUserInfoResponse]);


  return (
    <div>
      {saveUserInfoResponse.error ? (
        <Alert key={"success"} variant={"danger"}>
          Error getting user info records
        </Alert>
      ) : (
        <div></div>
      )}
      <Authenticator services={services} initialState="signUp">
        {({ signOut }) => <button onClick={(e) => {
          getUserInfoEmpty({}); 
          saveUserInfoEmpty({});
          signOut && signOut(e)}
          }>Sign out</button>}
      </Authenticator>
      <div>Welcome, {user?.attributes?.name}</div>
    </div>
  );
};

LoginContainer.defaultProps = {};

const mapDispatchToProps = (dispatch: Dispatch<any>) => {
  return {
    saveUserInfo: (saveUserInfoRequest: UserInfo) => dispatch(saveUserInfo(saveUserInfoRequest)),
      getUserInfo: () => dispatch(getUserInfo()),
      getUserInfoEmpty: () => dispatch(getUserInfoEmpty({})),
      saveUserInfoEmpty: () => dispatch(saveUserInfoEmpty({}))
  };
};

const mapStateToProps = (state: any) => {
  return {
    saveUserInfoResponse: state.saveUserInfoReducer,
    getUserInfoResponse: state.getUserInfoReducer
  };
};

type StateToPropsType = ReturnType<typeof mapStateToProps>;
type DispatchToPropsType = typeof mapDispatchToProps;

export default withAuthenticator(
  connect<StateToPropsType, DispatchToPropsType>(
    mapStateToProps,
    mapDispatchToProps
  )(LoginContainer)
);