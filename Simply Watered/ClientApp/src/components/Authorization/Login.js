import AuthorizeApi from "./AuthorizeApi";
import React, { Component } from "react";

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      redirect: null,
      fields: {},
      errors: {},
    };
  }

  onSubmit = async (e) => {
    e.preventDefault();
    const loginModel = {
      email: this.state.fields["inputEmail"],
      password: this.state.fields["inputPassword"],
    };

    await AuthorizeApi.login(loginModel);
    this.props.history.push("/regiongroups");
  };

  handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
  }

  render() {
    return (
      <>
        <form
          onSubmit={async (event) => {
            await this.onSubmit(event);
          }}
        >
          <div className="form-group">
            <label htmlFor="inputEmail">Email address</label>
            <input
              type="email"
              className="form-control"
              id="inputEmail"
              aria-describedby="emailHelp"
              placeholder="Enter email"
              onChange={this.handleChange.bind(this, "inputEmail")}
            />
          </div>
          <div className="form-group">
            <label htmlFor="inputPassword">Password</label>
            <input
              type="password"
              className="form-control"
              id="inputPassword"
              placeholder="Password"
              onChange={this.handleChange.bind(this, "inputPassword")}
            />
          </div>
          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </form>
      </>
    );
  }
}
