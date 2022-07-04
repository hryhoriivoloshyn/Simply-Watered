import React, { Component } from "react";
import { Link } from "react-router-dom";
import authService from "./api-authorization/AuthorizeService";
import { withRouter } from "react-router-dom";
import AuthLocalStorage from "./Authorization/AuthLocalStorage";

export class GroupList extends Component {
  static displayName = GroupList.name;

  constructor(props) {
    super(props);
    this.state = {
      regiongroups: [],
      loading: true,
      message: "",
      path: this.props.location.pathname,
    };
  }

  onClick(regiongroup) {
    this.onRemoveGroup(regiongroup);
  }

  onRemoveGroup = async (regiongroup) => {
    console.log("Проверка группы для удаления");

    let groupId = regiongroup.regionGroupId;
    if (regiongroup) {
      console.log("Удаление");
      let token = await authService.getAccessToken();
      console.log(token);
      let response = await fetch(`api${this.state.path}/${groupId}`, {
        method: "DELETE",
        headers: !token
          ? {
              "Content-Type": "application/json",
            }
          : {
              "Content-Type": "application/json",
              Authorization: `Bearer ${token}`,
            },
      }).then(async () => {
        await this.loadData();
      });
      console.log(response);
    }
  };

  componentDidMount() {
    this.loadData();
  }

  render() {
    let regiongroups = this.state.regiongroups;
    return (
      <>
        <h2 className="text-center">Групи ділянок</h2>
        <hr />

        <Link
          to="/regiongroups/add"
          className="btn btn-primary mx-3"
          role="button"
        >
          Додати групу
        </Link>

        <table
          className="table table-striped text-center mt-3"
          aria-labelledby="tabelLabel"
        >
          <thead>
            <tr>
              <th>Назва</th>
              <th>Опис</th>
              <th colSpan="3">Дії</th>
            </tr>
          </thead>
          <tbody>
            {regiongroups.map((regiongroup) => (
              <tr key={regiongroup.regionGroupId}>
                <td>{regiongroup.groupName}</td>
                <td>{regiongroup.regionGroupDescription}</td>
                <td>
                  <Link
                    className="btn btn-outline-primary"
                    role="button"
                    to={{
                      pathname: `${this.state.path}/${regiongroup.regionGroupId}/regions`,
                    }}
                  >
                    Переглянути ділянки
                  </Link>
                </td>
                <td>
                  <Link
                    className="btn btn-outline-primary"
                    role="button"
                    to={{
                      pathname: `${this.state.path}/${regiongroup.regionGroupId}/schedules`,
                    }}
                  >
                    Задати розклад зрошення
                  </Link>
                </td>
                <td>
                  <button
                    className="btn btn-outline-dark"
                    onClick={async () => {
                      await this.onRemoveGroup(regiongroup);
                    }}
                  >
                    Видалити
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </>
    );
  }

  //Загрузка данных
  async loadData() {
    const token = await AuthLocalStorage.getAccessToken();
    const response = await fetch("api/regiongroups", {
      headers: !token
        ? { "Content-Type": "application/json" }
        : {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
    });
    const data = await response.json();
    this.setState({ regiongroups: data, loading: false });
  }
}
