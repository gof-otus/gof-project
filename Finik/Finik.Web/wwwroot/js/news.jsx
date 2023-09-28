class News extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.news };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <div>
            <p>HeadLine <b>{this.state.data.headLine}</b></p>
            <p>Body {this.state.data.body}</p> 
        </div>;
    }
}

class NewsForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { headLine: "", body: "" };

        this.onAddNews = this.onAddNews.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.onHeadLineChange = this.onHeadLineChange.bind(this);
        this.onBodyChange = this.onBodyChange.bind(this);
    }
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ newss: data });
        }.bind(this);
        xhr.send();
    }
    // добавление объекта
    onAddNews(News) {
        if (News) {

            const data = new FormData();
            data.append("headLine", News.headLine);
            data.append("body", News.body); 
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    onSubmit(e) {
        e.preventDefault();
        var HeadLine = this.state.headLine;
        var Body = this.state.body; 
        if (!HeadLine || !Body) {
            return;
        }
        this.onAddNews({ headLine: HeadLine, body: Body });
        this.setState({ headLine: "", body: "" });
    }
    onHeadLineChange(e) {
        this.setState({ headLine: e.target.value });
    }
    onBodyChange(e) {
        this.setState({ body: e.target.value });
    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="headLine"
                        value={this.state.headLine}
                        onChange={this.onHeadLineChange} />
                </p>
                <p>
                    <input type="text"
                        placeholder="body"
                        value={this.state.body}
                        onChange={this.onBodyChange} />
                </p>
                <input type="submit" value="Сохранить" />
            </form>
        );
    }
}
class NewssList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { newss: [] };

        this.onRemoveNews = this.onRemoveNews.bind(this);
    }
    // загрузка данных
    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ newss: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // удаление объекта
    onRemoveNews(News) {

        if (News) {
            var url = this.props.apiUrl + "/" + News.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }
    render() {

        var remove = this.onRemoveNews;
        return <div>
            <h2>Список новостей</h2>
            <div>
                {
                    this.state.newss.map(function (news) {

                        return <News key={news.id} news={news} onRemove={remove} />
                    })
                }
            </div>
        </div>;
    }
}

 



ReactDOM.render(
    <NewssList apiUrl="/api/newss" />,
    document.getElementById("content")
);

