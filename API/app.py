from flask import Flask, render_template, request, redirect, url_for
from flask_sqlalchemy import SQLAlchemy
from flask.templating import render_template
from flask_migrate import Migrate, migrate
from flask import Flask, jsonify


app = Flask(__name__)
app.debug = False
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///site.db'
db = SQLAlchemy(app)
migrate = Migrate(app, db)

class User(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    username = db.Column(db.String(80), unique=True, nullable=False)
    password = db.Column(db.String(120), unique=False, nullable=False)

    def __init__(self, username, password):
        self.username = username
        self.password = password

"""
admin = User("PentaVR", "PentaVR1234*")
db.session.add(admin)
db.session.commit()
"""

class Question(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    question = db.Column(db.String(1000), nullable=False)
    choice_a = db.Column(db.String(500), nullable=False)
    choice_b = db.Column(db.String(500), nullable=False)
    choice_c = db.Column(db.String(500), nullable=False)
    choice_d = db.Column(db.String(500), nullable=False)
    country = db.Column(db.String(500), nullable=False)
    right_answer = db.Column(db.String(500), nullable=False)


@app.route('/questionsApi')
def get_questions():
    questions = Question.query.all()
    result = []
    for question in questions:
        question_data = {}
        question_data['id'] = question.id
        question_data['question'] = question.question
        question_data['choice_a'] = question.choice_a
        question_data['choice_b'] = question.choice_b
        question_data['choice_c'] = question.choice_c
        question_data['choice_d'] = question.choice_d
        question_data['country'] = question.country
        question_data['right_answer'] = question.right_answer
        result.append(question_data)
    return jsonify(result)


class Tip(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    tip = db.Column(db.String(500), nullable=False )
    country = db.Column(db.String(500), nullable=False)

@app.route('/tipsApi')
def get_tips():
    tips = Tip.query.all()
    result = []
    for tip in tips:
        tip_data = {}
        tip_data['id'] = tip.id
        tip_data['title'] = tip.title
        tip_data['content'] = tip.content
        result.append(tip_data)
    return jsonify(result)


@app.route('/')
def index():
    return render_template('login.html')

@app.route('/login', methods=['POST'])
def login():
    username = request.form['username']
    password = request.form['password']
    user = User.query.filter_by(username=username, password=password).first()
    if user:
        return redirect(url_for('home'))
    else:
        return render_template('login.html',mesaj="Password or username is incorrect")

@app.route('/home')
def home():
    return render_template('add_question.html')

@app.route('/add_question', methods=['POST'])
def add_question():
    question = request.form['question']
    choice_a = request.form['choice_a']
    choice_b = request.form['choice_b']
    choice_c = request.form['choice_c']
    choice_d = request.form['choice_d']
    country = request.form['country']
    right_answer = request.form['right_answer']
    new_question = Question(question, choice_a, choice_b, choice_c, choice_d, country, right_answer)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))


@app.route('/add_tip', methods=['POST'])
def add_tip():
    country = request.form['country']
    tip = request.form['tip']
    new_question = Tip(tip, country)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))



if __name__ == '__main__':
    app.run()



